using System;
using System.IO;

namespace SmartHouseApp.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logPath;
        
        public LoggerService()
        {
            _logPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs",
                $"smarthouse-{DateTime.Now:yyyy-MM-dd}.log"
            );
            
            Directory.CreateDirectory(Path.GetDirectoryName(_logPath));
        }
        
        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }
        
        public void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }
        
        public void LogError(string message, Exception ex = null)
        {
            string fullMessage = ex != null 
                ? $"{message}\n{ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}"
                : message;
            
            WriteLog("ERROR", fullMessage);
        }
        
        public void LogDeviceAction(string deviceName, string room, string action)
        {
            WriteLog("DEVICE", $"[{room}] {deviceName} - {action}");
        }
        
        public void LogRoomAction(string roomName, string action)
        {
            WriteLog("ROOM", $"{roomName} - {action}");
        }
        
        private void WriteLog(string level, string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level,-6}] {message}";
                
                lock (_logPath)  // Thread-safe file writing
                {
                    File.AppendAllText(_logPath, logEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                // Fail silently to not break application
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
