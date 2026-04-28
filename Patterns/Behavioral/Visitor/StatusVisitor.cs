namespace SmartHouseApp.Patterns.Behavioral.Visitor
{
     public class StatusVisitor : IVisitor
     {
          public void Visit(LightDevice device)
          {
               Console.WriteLine("Checking light status...");
          }
     }
}