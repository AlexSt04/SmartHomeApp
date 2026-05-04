using System;

namespace SmartHouseApp.Services
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex = null);
        void LogDeviceAction(string deviceName, string room, string action);
        void LogRoomAction(string roomName, string action);
    }
}
