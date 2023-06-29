namespace Utils.Interfaces.Observable
{
    public interface IObserver<T>
    {
        public void Update(T value);
    }
}