using SmartHouseApp.Models;
using SmartHouseApp.Services;
using System;

namespace SmartHouseApp.Patterns.Proxy
{
    public class DeviceProxy : Device
    {
        private readonly Device _realDevice;
        private readonly bool _authorized;
        private readonly ILoggerService _logger;
        
        public DeviceProxy(Device device, bool authorized, ILoggerService logger = null)
            : base(device.Name, device.Room)
        {
            _realDevice = device;
            _authorized = authorized;
            _logger = logger;
        }
        
        public override void TurnOn()
        {
            if (_authorized)
            {
                _realDevice.TurnOn();
                _logger?.LogDeviceAction(Name, Room, "Turned ON (authorized)");
            }
            else
            {
                _logger?.LogWarning($"Access DENIED - {Name} in {Room}");
                Console.WriteLine("❌ Access denied");
            }
        }
        
        public override void TurnOff()
        {
            if (_authorized)
            {
                _realDevice.TurnOff();
                _logger?.LogDeviceAction(Name, Room, "Turned OFF (authorized)");
            }
            else
            {
                _logger?.LogWarning($"Access DENIED - {Name} in {Room}");
                Console.WriteLine("❌ Access denied");
            }
        }
        
        public override Device Clone()
        {
            return new DeviceProxy(_realDevice.Clone(), _authorized, _logger);
        }
        
        public override string GetStatus()
        {
            return _authorized ? _realDevice.GetStatus() : "LOCKED";
        }
        
        public override bool IsActive => _authorized && _realDevice.IsActive;
    }
}