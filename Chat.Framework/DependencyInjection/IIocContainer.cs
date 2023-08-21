namespace Chat.Framework.DependencyInjection
{
    public interface IIocContainer
    {
        T? GetService<T>();
        T? GetService<T>(string name);
        List<T> GetServices<T>();
    }
}
