using LiteDB;
using SmartHouseApp.Models;

namespace SmartHouseApp.Services
{
     /// <summary>
     /// Repository dedicat scenelor custom salvate de utilizator.
     /// Folosește același fișier LiteDB ca DatabaseService (smarthome.db),
     /// colecția "scenes".
     /// </summary>
     public class SceneRepository
     {
          private readonly string _dbPath;

          public SceneRepository()
          {
               string baseDir = AppDomain.CurrentDomain.BaseDirectory;
               _dbPath = System.IO.Path.Combine(baseDir, "Data", "smarthome.db");
          }

          // ── LOAD ──────────────────────────────────────────────────────────
          public List<Scene> LoadCustomScenes()
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<SceneRecord>("scenes");
                    return col.FindAll().Select(ToScene).ToList();
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[SceneRepo] Load error: {ex.Message}");
                    return new List<Scene>();
               }
          }

          // ── SAVE ──────────────────────────────────────────────────────────
          public void SaveScene(Scene scene)
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<SceneRecord>("scenes");
                    col.Upsert(ToRecord(scene));
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[SceneRepo] Save error: {ex.Message}");
               }
          }

          // ── DELETE ────────────────────────────────────────────────────────
          public void DeleteScene(string sceneId)
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<SceneRecord>("scenes");
                    col.Delete(sceneId);
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[SceneRepo] Delete error: {ex.Message}");
               }
          }

          // ── MAPPING ───────────────────────────────────────────────────────
          private static SceneRecord ToRecord(Scene s) => new()
          {
               Id = s.Id,
               Name = s.Name,
               Icon = s.Icon,
               Description = s.Description,
               AccentColor = s.AccentColor,
               Actions = s.Actions.Select(a => new SceneActionRecord
               {
                    DeviceType = a.DeviceType,
                    RoomFilter = a.RoomFilter,
                    ActionType = (int)a.ActionType,
                    TemperatureValue = a.TemperatureValue
               }).ToList()
          };

          private static Scene ToScene(SceneRecord r) => new()
          {
               Id = r.Id,
               Name = r.Name,
               Icon = r.Icon,
               Description = r.Description,
               AccentColor = r.AccentColor,
               IsPreset = false,
               Actions = r.Actions.Select(a => new SceneAction
               {
                    DeviceType = a.DeviceType,
                    RoomFilter = a.RoomFilter,
                    ActionType = (SceneActionType)a.ActionType,
                    TemperatureValue = a.TemperatureValue
               }).ToList()
          };
     }

     // ── LiteDB Records ────────────────────────────────────────────────────
     public class SceneRecord
     {
          [BsonId] public string Id { get; set; } = "";
          public string Name { get; set; } = "";
          public string Icon { get; set; } = "🏠";
          public string Description { get; set; } = "";
          public string AccentColor { get; set; } = "#10B981";
          public List<SceneActionRecord> Actions { get; set; } = new();
     }

     public class SceneActionRecord
     {
          public string DeviceType { get; set; } = "All";
          public string RoomFilter { get; set; } = "";
          public int ActionType { get; set; }
          public int TemperatureValue { get; set; } = 22;
     }
}