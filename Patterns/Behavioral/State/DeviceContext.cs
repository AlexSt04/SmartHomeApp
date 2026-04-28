namespace SmartHouseApp.Patterns.Behavioral.State
{
     public class DeviceContext
     {
          public IDeviceState State { get; set; }

          public void Request()
          {
               State.Handle(this);
          }
     }
}