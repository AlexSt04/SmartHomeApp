namespace SmartHouseApp.Patterns.Behavioral.State
{
     public class OffState : IDeviceState
     {
          public void Handle(DeviceContext context)
          {
               Console.WriteLine("Device is OFF → switching ON");
               context.State = new OnState();
          }
     }
}