namespace SmartHouseApp.Patterns.Behavioral.Mediator
{
     public abstract class DeviceColleague
     {
          protected IMediator mediator;

          public DeviceColleague(IMediator mediator)
          {
               this.mediator = mediator;
          }
     }
}