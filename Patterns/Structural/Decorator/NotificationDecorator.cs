using SmartHouseApp.Models;

namespace SmartHouseApp.Patterns.Decorator
{
     public class NotificationDecorator : DeviceDecorator
     {
          public NotificationDecorator(Device device) : base(device) { }

          public override void TurnOn()
          {
               _device.TurnOn();
               Console.WriteLine("🔔 Notification: Device turned ON");
          }

          public override void TurnOff()
          {
               _device.TurnOff();
               Console.WriteLine("🔕 Notification: Device turned OFF");
          }

          public override Device Clone()
          {
               return new NotificationDecorator(_device.Clone());
          }
     }
}