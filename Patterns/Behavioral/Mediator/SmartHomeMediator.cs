namespace SmartHouseApp.Patterns.Behavioral.Mediator
{
     public class SmartHomeMediator : IMediator
     {
          public void Notify(string message, DeviceColleague sender)
          {
               Console.WriteLine("Mediator received: " + message);
          }
     }
}