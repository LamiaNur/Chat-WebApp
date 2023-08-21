using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Extensions
{
    public static class ServiceProviderExtension
    {
        public static T? GetService<T>(this IServiceProvider serviceProvider, string name)
        {
            var services = serviceProvider.GetServices<T>();
            return services.FirstOrDefault(o => o?.GetType()?.Name == name);
        }

        public static List<Assembly> GetAddedAssemblies(this IServiceProvider serviceProvider)
        {
            return ServiceCollectionExtension.GetAddedAssemblies();
        }
    }
}
