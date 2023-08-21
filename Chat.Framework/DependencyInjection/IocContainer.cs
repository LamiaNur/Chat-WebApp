using System.Composition.Hosting;
using Chat.Framework.Attributes;
using Chat.Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.DependencyInjection
{
    [ServiceRegister(typeof(IIocContainer), ServiceLifetime.Singleton)]
    public class IocContainer : IIocContainer
    {
        private readonly CompositionHost _container;
        private readonly IServiceProvider _serviceProvider;

        public IocContainer(IServiceProvider serviceProvider)
        {
            Console.WriteLine("DIService Constructor\n");
            _serviceProvider = serviceProvider;

            var containerConfiguration = new ContainerConfiguration();
            var assembles = serviceProvider.GetAddedAssemblies();
            containerConfiguration.WithAssemblies(assembles);
            _container = containerConfiguration.CreateContainer();
            
            Console.WriteLine("DIService _container Created\n");
        }

        public T? GetService<T>()
        {
            try
            {
                var service = _container.GetExport<T>();
                Console.WriteLine($"GetService Success of type : {typeof(T).Name} with container.\n");
                return service;
            }
            catch (Exception)
            {
                Console.WriteLine($"GetService Failed of type : {typeof(T).Name} with container.\n");
                var service = _serviceProvider.GetService<T>();
                Console.WriteLine($"GetService Success of type : {typeof(T).Name} with _serviceProvider.\n");
                return service;
            }
        }

        public T? GetService<T>(string name)
        {
            try
            {
                var service = _container.GetExport<T>(name);
                Console.WriteLine($"GetService Success of type : {typeof(T).Name} and name : {name} with container.\n");
                return service;
            }
            catch (Exception)
            {
                Console.WriteLine($"GetService Failed of type : {typeof(T).Name} and name : {name} with container.\n");
                var service = _serviceProvider.GetService<T>(name);
                Console.WriteLine($"GetService Success of type : {typeof(T).Name} and name : {name} by searching with all the instances\n");
                return service;
            }
        }

        public List<T> GetServices<T>()
        {
            var allServices = new List<T>();
            try
            {
                var services = _container.GetExports<T>().ToList();
                Console.WriteLine($"GetServices Success of type {typeof(T).Name} with container. Services Count : {services.Count}\n");
                allServices.AddRange(services);
            }
            catch (Exception)
            {
                Console.WriteLine($"GetServices Failed of type {typeof(T).Name} with container\n");
                var services = _serviceProvider.GetServices<T>().ToList();
                Console.WriteLine($"GetServices Success of type {typeof(T).Name} with _serviceProvider. Services Count : {services.Count}\n");
                allServices.AddRange(services);
            }
            return allServices;
        }
    }
}