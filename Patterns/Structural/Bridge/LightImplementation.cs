namespace SmartHouseApp.Patterns.Bridge
{
     public class LightImplementation : IDeviceImplementation
     {
          public void ExecuteOn()
          {
               Console.WriteLine("Light ON (Bridge)");
          }

          public void ExecuteOff()
          {
               Console.WriteLine("Light OFF (Bridge)");
          }
     }
}