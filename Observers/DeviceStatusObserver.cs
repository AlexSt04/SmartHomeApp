using System;

namespace SmartHouseApp.Observers
{
    public class DeviceStatusObserver : IObserver
    {
        private readonly Action<ObserverNotification> _onNotification;
        
        public DeviceStatusObserver(Action<ObserverNotification> onNotification)
        {
            _onNotification = onNotification;
        }
        
        public void Update(ObserverNotification notification)
        {
            _onNotification?.Invoke(notification);
        }
    }
}
