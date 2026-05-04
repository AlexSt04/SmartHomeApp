using SmartHouseApp.Interfaces;
using System;

namespace SmartHouseApp.Models
{
    public abstract class Device : IPrototype<Device>, IDeviceStatus
    {
        public string Name { get; set; }
        public string Room { get; set; }

        // Event-based notification
        public event EventHandler<DeviceStatusChangedEventArgs> StatusChanged;

        protected Device(string name, string room)
        {
            Name = name;
            Room = room;
        }

        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, new DeviceStatusChangedEventArgs 
            { 
                Status = GetStatus(),
                Timestamp = DateTime.Now
            });
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
        public abstract Device Clone();

        public virtual string GetStatus() => "Unknown";
        public virtual bool IsActive => false;
    }

    public class DeviceStatusChangedEventArgs : EventArgs
    {
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}