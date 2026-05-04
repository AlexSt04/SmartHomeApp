using System;
using System.Collections.Generic;

namespace SmartHouseApp.Observers
{
    public interface IObserver
    {
        void Update(ObserverNotification notification);
    }
    
    public class ObserverNotification
    {
        public string EventType { get; set; }  // "DeviceTurnedOn", "RoomAdded", etc
        public object Source { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
