using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Attributes
{
    public class ServiceRegisterAttribute : Attribute
    {
        public Type ServiceType { get; set; }
        public ServiceLifetime ServiceLifetime { get; set; }
        public ServiceRegisterAttribute(Type serviceType, ServiceLifetime serviceLifeTime)
        {
            ServiceType = serviceType;
            ServiceLifetime = serviceLifeTime;
        }
    }
}
