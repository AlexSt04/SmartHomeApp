namespace SmartHouseApp.Patterns.Behavioral.State
{
     public class OnState : IDeviceState
     {
          public void Handle(DeviceContext context)
          {
               Console.WriteLine("Device is ON → switching OFF");
               context.State = new OffState();
          }
     }
}