namespace SmartHouseApp.Patterns.Behavioral.TemplateMethod
{
     public abstract class DeviceTemplate
     {
          public void Execute()
          {
               Authenticate();
               PerformAction();
               Log();
          }

          protected void Authenticate()
          {
               Console.WriteLine("Auth OK");
          }

          protected abstract void PerformAction();

          protected void Log()
          {
               Console.WriteLine("Action logged");
          }
     }
}