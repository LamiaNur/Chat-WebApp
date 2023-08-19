namespace Chat.Framework.Observer
{
    public class Observable<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> _observers = new();

        public void Subscribe(IObserver<T> observer)
        {
            if (_observers.IndexOf(observer) == -1)
            {
                _observers.Add(observer);
            }
        }

        public void UnSubscribe(IObserver<T> observer)
        {
            if (_observers.IndexOf(observer) != -1)
            {
                _observers.Remove(observer);
            }
        }

        public async Task NotifyAsync(T data)
        {
            foreach (var observer in _observers)
            {
                await observer.ObserveAsync(data);
            }
        }
    }
}
