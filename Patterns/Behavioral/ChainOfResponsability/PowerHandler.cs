namespace SmartHouseApp.Patterns.Behavioral.ChainOfResponsibility
{
     public class PowerHandler : Handler
     {
          public override void Handle(string request)
          {
               if (request == "POWER")
                    Console.WriteLine("Power handled");
               else
                    next?.Handle(request);
          }
     }
}