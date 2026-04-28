using SmartHouseApp.Models;

namespace SmartHouseApp.Patterns.Proxy
{
     public class DeviceProxy : Device
     {
          private Device _realDevice;
          private bool _authorized;

          public DeviceProxy(Device device, bool authorized)
              : base(device.Name, device.Room)
          {
               _realDevice = device;
               _authorized = authorized;
          }

          public override void TurnOn()
          {
               if (_authorized)
                    _realDevice.TurnOn();
               else
                    Console.WriteLine("Access denied");
          }

          public override void TurnOff()
          {
               if (_authorized)
                    _realDevice.TurnOff();
               else
                    Console.WriteLine("Access denied");
          }

          public override Device Clone()
          {
               return new DeviceProxy(_realDevice.Clone(), _authorized);
          }
     }
}