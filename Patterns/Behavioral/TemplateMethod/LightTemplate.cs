namespace SmartHouseApp.Patterns.Behavioral.TemplateMethod
{
     public class LightTemplate : DeviceTemplate
     {
          protected override void PerformAction()
          {
               Console.WriteLine("Light turned ON");
          }
     }
}