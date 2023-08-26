namespace Chat.Framework.Observer
{
    public interface IObservable<T>
    {
        Task NotifyAsync(T data);
        void Subscribe(IObserver<T> observer);
        void UnSubscribe(IObserver<T> observer);
    }
}
