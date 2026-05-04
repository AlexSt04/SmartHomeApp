namespace SmartHouseApp.Models
{
    public interface IDeviceStatus
    {
        string GetStatus();
        bool IsActive { get; }
    }
}
