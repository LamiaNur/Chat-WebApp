using System.Collections.Concurrent;
using System.Reflection;
using Chat.Framework.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Extensions
{
    public static class ServiceProviderExtension
    {
        private static readonly ConcurrentDictionary<string, object?> SingletonServices = new();

        public static T? GetService<T>(this IServiceProvider serviceProvider, string name)
        {
            if (SingletonServices.TryGetValue(GetServiceNameKey(typeof(T), name), out var value))
            {
                Console.WriteLine($"Found the instance of {name} of the Singleton Dictionary\n");
                return (T?)value;
            }
            
            Console.WriteLine($"Not Found the instance of {name} of the Singleton Dictionary\n");

            T? resolvedService = default;
            var services = serviceProvider.GetServices<T>().ToList();
            
            Console.WriteLine($"Iterating over {services.Count} implementation to find {name}\n");
            foreach (var service in services)
            {
                TryForServiceCache(service);
                if (name != service?.GetType().Name) continue;

                Console.WriteLine($"Implementation found of service {name} ");
                
                resolvedService = service;
            }
            return resolvedService;
        }
        
        public static List<Assembly> GetAddedAssemblies(this IServiceProvider serviceProvider)
        {
            return ServiceCollectionExtension.GetAddedAssemblies();
        }

        private static bool TryForServiceCache<T>(T service)
        {
            var type = service?.GetType();
            if (type == null) return false;

            var serviceRegisterAttribute = type.GetCustomAttribute<ServiceRegisterAttribute>();

            if (serviceRegisterAttribute == null || 
                serviceRegisterAttribute.ServiceLifetime != ServiceLifetime.Singleton) 
                return false;

            return SingletonServices.TryAdd(GetServiceNameKey(serviceRegisterAttribute.ServiceType, type.Name), service);
        }

        private static string GetServiceNameKey(Type serviceType, string name)
        {
            return serviceType.Name + "." + name;
        }
    }
}
