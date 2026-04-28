namespace SmartHouseApp.Patterns.Behavioral.ChainOfResponsibility
{
     public class SecurityHandler : Handler
     {
          public override void Handle(string request)
          {
               if (request == "SECURITY")
                    Console.WriteLine("Security handled");
               else
                    next?.Handle(request);
          }
     }
}