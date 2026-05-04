using System;

namespace SmartHouseApp.Services
{
    public class ValidationService
    {
        private const int MinRoomNameLength = 1;
        private const int MaxRoomNameLength = 50;
        private const int MaxRooms = 20;
        private const int MinTemperature = 16;
        private const int MaxTemperature = 30;
        
        public (bool IsValid, string ErrorMessage) ValidateRoomName(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
                return (false, "Room name cannot be empty");
            
            if (roomName.Length < MinRoomNameLength || roomName.Length > MaxRoomNameLength)
                return (false, $"Room name must be between {MinRoomNameLength} and {MaxRoomNameLength} characters");
            
            return (true, "");
        }
        
        public (bool IsValid, string ErrorMessage) ValidateTemperature(int temperature)
        {
            if (temperature < MinTemperature || temperature > MaxTemperature)
                return (false, $"Temperature must be between {MinTemperature}°C and {MaxTemperature}°C");
            
            return (true, "");
        }
        
        public (bool IsValid, string ErrorMessage) ValidateRoomCount(int currentCount)
        {
            if (currentCount >= MaxRooms)
                return (false, $"Maximum {MaxRooms} rooms allowed");
            
            return (true, "");
        }
    }
}
