using System.Reflection;
using Chat.Framework.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Extensions
{
    public static class ServiceCollectionExtension
    {
        private static readonly Dictionary<string, Assembly> AssemblyLists = new();
        private static int _counter = 0;

        public static List<Assembly> GetAddedAssemblies()
        {
            return AssemblyLists.Values.ToList();
        }

        public static void AddAttributeRegisteredServices(this IServiceCollection services)
        {
            if (_counter != 0) return;
            _counter++;

            var assemblies = GetAddedAssemblies();
            foreach (var assembly in assemblies)
            {
                var exportedTypes = assembly.GetExportedTypes();
                foreach (var type in exportedTypes)
                {
                    if (!type.IsClass || type.IsAbstract || type.IsInterface) continue;

                    var serviceRegisterAttribute = type.GetCustomAttribute<ServiceRegisterAttribute>();
                    
                    if (serviceRegisterAttribute == null) continue;

                    if (!type.IsAssignableTo(serviceRegisterAttribute.ServiceType)) continue;
                    
                    switch (serviceRegisterAttribute.ServiceLifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(serviceRegisterAttribute.ServiceType, type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(serviceRegisterAttribute.ServiceType, type);
                            break;
                        case ServiceLifetime.Transient:
                        default:
                            services.AddTransient(serviceRegisterAttribute.ServiceType, type);
                            break;
                    }
                }
            }
        }
        
        public static void AddAllAssemblies(this IServiceCollection services, string assemblyPrefix)
        {
            var entryAssemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (!string.IsNullOrEmpty(entryAssemblyLocation))
            {
                AddAllAssemblies(entryAssemblyLocation, assemblyPrefix);
            }
            var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrEmpty(executingAssemblyLocation))
            {
                AddAllAssemblies(executingAssemblyLocation, assemblyPrefix);
            }
        }

        private static void AddAllAssemblies(string location, string assemblyPrefix)
        {
            if (string.IsNullOrEmpty(location)) return;
            var files = Directory.GetFiles(location);
            foreach (var file in files)
            {
                try
                {
                    var fileInfo = new FileInfo(file);

                    if (!fileInfo.Name.StartsWith(assemblyPrefix, StringComparison.InvariantCultureIgnoreCase) ||
                        fileInfo.Extension != ".dll") continue;

                    var assemblyName = Path.GetFileNameWithoutExtension(file);
                    if (string.IsNullOrEmpty(assemblyName)) continue;

                    var assembly = Assembly.Load(assemblyName);
                    AddAssembly(assembly);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void AddAssembly(Assembly assembly)
        {
            if (string.IsNullOrEmpty(assembly.FullName) ||
                AssemblyLists.ContainsKey(assembly.FullName)) return;

            AssemblyLists.Add(assembly.FullName, assembly);
            Console.WriteLine($"Adding Assembly {assembly.FullName}\n");
        }
    }
}
