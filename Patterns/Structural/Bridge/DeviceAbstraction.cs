namespace SmartHouseApp.Patterns.Bridge
{
     public abstract class DeviceAbstraction
     {
          protected IDeviceImplementation implementation;

          protected DeviceAbstraction(IDeviceImplementation impl)
          {
               implementation = impl;
          }

          public abstract void TurnOn();
          public abstract void TurnOff();
     }
}