namespace SmartHouseApp.Patterns.Behavioral.Mediator
{
     public interface IMediator
     {
          void Notify(string message, DeviceColleague sender);
     }
}