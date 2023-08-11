using System.Composition.Hosting;
using System.Reflection;

namespace Chat.Api.CoreModule.Services
{
    public sealed class DIService
    {
        private static DIService? _instance;
        private static readonly object _lockObject = new();
        private CompositionHost? _container;
        private readonly ContainerConfiguration _containerConfiguration;
        private readonly List<string> _assemblyLists;
        private IServiceProvider? ServiceProvider { get; set; }
        
        private DIService()
        {
            Console.WriteLine("DIService Constructor\n");
            _containerConfiguration = new ContainerConfiguration();
            _assemblyLists = new List<string>();
        }

        public static DIService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new DIService();
                            Console.WriteLine("DIService instance created\n");
                        }
                    }
                }
                return _instance;
            }
        }

        public void Initialize(IServiceProvider serviceProvider, string projectPrefix = "Chat")
        {
            ServiceProvider = serviceProvider;
            AddAllAssemblies(projectPrefix);
            CreateContainer();
        }

        private void AddAllAssemblies(string prefix)
        {
            var entryAssemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (!string.IsNullOrEmpty(entryAssemblyLocation))
            {
                AddAllAssemblies(entryAssemblyLocation, prefix);
            }
            var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrEmpty(executingAssemblyLocation))
            {
                AddAllAssemblies(executingAssemblyLocation, prefix);
            }
        }

        private void AddAllAssemblies(string location, string prefix)
        {
            if (string.IsNullOrEmpty(location) == false)
            {
                var files = Directory.GetFiles(location);
                foreach (var file in files)
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        if (fileInfo.Name.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase) &&
                            fileInfo.Extension == ".dll")
                        {
                            var assemblyName = Path.GetFileNameWithoutExtension(file);
                            if (string.IsNullOrEmpty(assemblyName) == false)
                            {
                                var assembly = Assembly.Load(assemblyName);
                                if (assembly != null)
                                {
                                    AddAssembly(assembly);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private void AddAssembly<T>()
        {
            AddAssembly(typeof(T).Assembly);
        }

        private void AddAssembly(Assembly assembly)
        {
            
            if (string.IsNullOrEmpty(assembly.FullName) || _assemblyLists.Contains(assembly.FullName))
            {
                return;
            }
            _containerConfiguration.WithAssembly(assembly);
            _assemblyLists.Add(assembly.FullName);
            Console.WriteLine($"Adding Assembly {assembly.FullName}\n");
        }

        private void CreateContainer()
        {
            if (_container == null)
            {
                lock(_lockObject)
                {
                    if (_container == null)
                    {
                        _container = _containerConfiguration.CreateContainer();
                        Console.WriteLine("DIService Container Created\n");
                    }
                }
            }
        }

        public T GetService<T>()
        {
            CreateContainer();
        
            lock (_lockObject)
            {
                try
                {
                    if (_container == null)
                    {
                        Console.WriteLine("Container not set\n");
                        throw new Exception();
                    }
                    var service = _container.GetExport<T>();
                    Console.WriteLine($"GetService Success of type : {typeof(T).Name} with container.\n");
                    return service;
                }
                catch (Exception)
                {
                    Console.WriteLine($"GetService Failed of type : {typeof(T).Name} with container.\n");
                    try
                    {
                        if (ServiceProvider == null) 
                        {
                            Console.WriteLine("ServiceProvider not set\n");
                            throw;
                        }
                        var service = ServiceProvider.GetService<T>();
                        if (service == null)
                        {
                            throw new Exception();
                        }
                        Console.WriteLine($"GetService Success of type : {typeof(T).Name} with ServiceProvider.\n");
                        return service;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"GetService Failed of type : {typeof(T).Name} with ServiceProvider.\n");
                        throw new Exception($"GetService Failed of type : {typeof(T).Name} with ServiceProvider.\n");
                    }
                }
            }
        }

        public T GetService<T>(string name)
        {
            CreateContainer();
            lock (_lockObject)
            {
                try
                {
                    if (_container == null)
                    {
                        Console.WriteLine("Container not set\n");
                        throw new Exception();
                    }
                    var service = _container.GetExport<T>(name);
                    Console.WriteLine($"GetService Success of type : {typeof(T).Name} and name : {name} with container.\n");
                    return service;
                }
                catch (Exception)
                {
                    Console.WriteLine($"GetService Failed of type : {typeof(T).Name} and name : {name} with container.\n");
                    var services = GetServices<T>();
                    foreach (var service in services)
                    {
                        if (service?.GetType()?.Name == name)
                        {
                            Console.WriteLine($"GetService Success of type : {typeof(T).Name} and name : {name} by searching with all the instances\n");
                            return service;
                        }
                    }
                    Console.WriteLine($"GetService Failed of type : {typeof(T).Name} and name : {name} by searching with all the instances\n");
                    throw new Exception($"GetService Failed of type : {typeof(T).Name} and name : {name} by searching with all the instances\n");
                }
            }
        }

        public List<T> GetServices<T>()
        {
            CreateContainer();
            lock (_lockObject)
            {
                try
                {
                    if (_container == null)
                    {
                        Console.WriteLine("Container not set\n");
                        throw new Exception();
                    }
                    var services = _container.GetExports<T>().ToList<T>();
                    Console.WriteLine($"GetServices Success of type {typeof(T).Name} with container. Services Count : {services.Count}\n");
                    return services;
                }
                catch (Exception)
                {
                    Console.WriteLine($"GetServices Failed of type {typeof(T).Name} with container\n");
                    try
                    { 
                        if (ServiceProvider == null) 
                        {
                            Console.WriteLine("ServiceProvider not set\n");
                            throw;
                        }
                        var services = ServiceProvider.GetServices<T>().ToList<T>();
                        Console.WriteLine($"GetServices Success of type {typeof(T).Name} with ServiceProvider. Services Count : {services.Count}\n");
                        return services;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"GetServices Failed of type {typeof(T).Name} with ServiceProvider");
                        return new List<T>();
                    }
                }
            }
        }

        public IConfiguration GetConfiguration()
        {
            if (ServiceProvider == null)
            {
                throw new Exception("Service Provider not set");
            }
            var config = ServiceProvider.GetService<IConfiguration>();
            if (config == null)
            {
                throw new Exception("Problem Getting Configuration");
            }
            Console.WriteLine("Get Configuration Success\n");
            return config;
        }
    }
}