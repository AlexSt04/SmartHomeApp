namespace SmartHouseApp.Patterns.Behavioral.State
{
     public interface IDeviceState
     {
          void Handle(DeviceContext context);
     }
}