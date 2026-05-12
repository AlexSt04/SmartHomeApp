namespace SmartHouseApp.Models
{
     /// <summary>
     /// Factory pentru cele 4 scene predefinite.
     /// Fiecare returnează o instanță nouă la fiecare apel.
     /// </summary>
     public static class PresetScenes
     {
          public static Scene MovieNight() => new()
          {
               Id = "preset_movie",
               Name = "Movie Night",
               Icon = "🎬",
               Description = "Stinge luminile, AC la 20°C, pornește TV",
               AccentColor = "#7C3AED",
               IsPreset = true,
               Actions = new List<SceneAction>
            {
                new() { DeviceType = "Light",          ActionType = SceneActionType.TurnOff },
                new() { DeviceType = "AirConditioner", ActionType = SceneActionType.SetTemperature, TemperatureValue = 20 },
                new() { DeviceType = "SmartTV",        ActionType = SceneActionType.TurnOn },
            }
          };

          public static Scene Morning() => new()
          {
               Id = "preset_morning",
               Name = "Morning",
               Icon = "🌅",
               Description = "Aprinde luminile, thermostat la 22°C",
               AccentColor = "#F59E0B",
               IsPreset = true,
               Actions = new List<SceneAction>
            {
                new() { DeviceType = "Light",      ActionType = SceneActionType.TurnOn },
                new() { DeviceType = "Thermostat", ActionType = SceneActionType.SetTemperature, TemperatureValue = 22 },
            }
          };

          public static Scene Away() => new()
          {
               Id = "preset_away",
               Name = "Away",
               Icon = "🏃",
               Description = "Stinge tot, încuie ușile",
               AccentColor = "#EF4444",
               IsPreset = true,
               Actions = new List<SceneAction>
            {
                new() { DeviceType = "All",      ActionType = SceneActionType.TurnOff },
                new() { DeviceType = "DoorLock", ActionType = SceneActionType.Lock },
            }
          };

          public static Scene Sleep() => new()
          {
               Id = "preset_sleep",
               Name = "Sleep",
               Icon = "🌙",
               Description = "Stinge luminile, thermostat la 18°C",
               AccentColor = "#1D4ED8",
               IsPreset = true,
               Actions = new List<SceneAction>
            {
                new() { DeviceType = "Light",      ActionType = SceneActionType.TurnOff },
                new() { DeviceType = "SmartTV",    ActionType = SceneActionType.TurnOff },
                new() { DeviceType = "Thermostat", ActionType = SceneActionType.SetTemperature, TemperatureValue = 18 },
            }
          };

          public static List<Scene> All() => new()
        {
            MovieNight(), Morning(), Away(), Sleep()
        };
     }
}