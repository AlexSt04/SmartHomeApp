using SmartHouseApp.Models;
using System;

namespace SmartHouseApp.Patterns.Decorator
{
    public class NotificationDecorator : DeviceDecorator
    {
        public NotificationDecorator(Device device) : base(device) { }
        
        public override void TurnOn()
        {
            _device.TurnOn();
            
            // Visual feedback
            Console.WriteLine($"🔔 NOTIFICATION: {Name} in {Room} turned ON");
            
            // Trigger event for Observer pattern
            OnStatusChanged();
        }
        
        public override void TurnOff()
        {
            _device.TurnOff();
            
            Console.WriteLine($"🔕 NOTIFICATION: {Name} in {Room} turned OFF");
            
            OnStatusChanged();
        }
        
        public override Device Clone()
        {
            return new NotificationDecorator(_device.Clone());
        }
        
        public override string GetStatus()
        {
            return $"🔔 {_device.GetStatus()}";  // Add notification indicator
        }
        
        public override bool IsActive => _device.IsActive;
    }
}