using SmartHouseApp.Models;

namespace SmartHouseApp.Patterns.Decorator
{
     public abstract class DeviceDecorator : Device
     {
          protected Device _device;

          protected DeviceDecorator(Device device)
              : base(device.Name, device.Room)
          {
               _device = device;
          }
     }
}