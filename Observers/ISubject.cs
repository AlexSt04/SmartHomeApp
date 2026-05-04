namespace SmartHouseApp.Observers
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void Notify(ObserverNotification notification);
    }
}
