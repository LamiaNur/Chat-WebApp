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
            var serviceNameKey = name;
            if (SingletonServices.TryGetValue(serviceNameKey, out var value))
            {
                Console.WriteLine($"Found the instance of {name} of the Singleton Dictionary\n");
                return (T?)value;
            }
            Console.WriteLine($"Not Found the instance of {name} of the Singleton Dictionary\n");

            T? resolveService = default;
            var services = serviceProvider.GetServices<T>().ToList();
            
            Console.WriteLine($"Iterating over {services.Count} implementation to find {name}\n");
            foreach (var service in services)
            {
                var type = service?.GetType();
                if (type == null) continue;

                var serviceName = type.Name;
                if (string.IsNullOrEmpty(serviceName)) continue;

                var serviceRegisterAttribute = type.GetCustomAttribute<ServiceRegisterAttribute>();
                if (serviceRegisterAttribute != null && serviceRegisterAttribute.ServiceLifetime == ServiceLifetime.Singleton)
                {
                    SingletonServices.TryAdd(serviceName, service);
                }
                if (name == serviceName) resolveService = service;
            }
            return resolveService;
        }

        public static List<Assembly> GetAddedAssemblies(this IServiceProvider serviceProvider)
        {
            return ServiceCollectionExtension.GetAddedAssemblies();
        }
    }
}
