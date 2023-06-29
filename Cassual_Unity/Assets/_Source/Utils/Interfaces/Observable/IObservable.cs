namespace Utils.Interfaces.Observable
{
    public interface IObservable<T>
    {
        public void Add(IObserver<T> newObserver);
        public void Remove(IObserver<T> observer);
    }
}