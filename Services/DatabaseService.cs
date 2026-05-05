using LiteDB;
using SmartHouseApp.Models;
using System.IO;

namespace SmartHouseApp.Services
{
     /// <summary>
     /// Serviciu de persistare folosind LiteDB (embedded NoSQL).
     /// Fișierul smarthome.db se creează automat lângă .exe.
     /// Nu necesită instalare server, nu necesită SQL.
     /// </summary>
     public class DatabaseService : IDatabaseService
     {
          private readonly string _dbPath;

          public DatabaseService()
          {
               string baseDir = AppDomain.CurrentDomain.BaseDirectory;
               Directory.CreateDirectory(Path.Combine(baseDir, "Data"));
               _dbPath = Path.Combine(baseDir, "Data", "smarthome.db");
          }

          public string GetDatabasePath() => _dbPath;

          // ─────────────────────────────────────────────────────────────────
          //  LOAD
          // ─────────────────────────────────────────────────────────────────
          public List<Room> LoadRooms()
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<RoomRecord>("rooms");
                    var records = col.FindAll().ToList();
                    return records.Select(RecordToRoom).ToList();
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[DB] LoadRooms error: {ex.Message}");
                    return new List<Room>();
               }
          }

          // ─────────────────────────────────────────────────────────────────
          //  SAVE ALL (snapshot complet)
          // ─────────────────────────────────────────────────────────────────
          public void SaveAllRooms(List<Room> rooms)
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<RoomRecord>("rooms");
                    col.DeleteAll();
                    var records = rooms.Select(RoomToRecord).ToList();
                    col.InsertBulk(records);
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[DB] SaveAllRooms error: {ex.Message}");
               }
          }

          // ─────────────────────────────────────────────────────────────────
          //  UPSERT (adaugă sau actualizează o cameră)
          // ─────────────────────────────────────────────────────────────────
          public void UpsertRoom(Room room)
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<RoomRecord>("rooms");
                    col.Upsert(RoomToRecord(room));
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[DB] UpsertRoom error: {ex.Message}");
               }
          }

          // ─────────────────────────────────────────────────────────────────
          //  DELETE
          // ─────────────────────────────────────────────────────────────────
          public void DeleteRoom(string roomName)
          {
               try
               {
                    using var db = new LiteDatabase(_dbPath);
                    var col = db.GetCollection<RoomRecord>("rooms");
                    col.Delete(roomName);
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[DB] DeleteRoom error: {ex.Message}");
               }
          }

          // ─────────────────────────────────────────────────────────────────
          //  CLEAR ALL
          // ─────────────────────────────────────────────────────────────────
          public void ClearAll()
          {
               try
               {
                    if (File.Exists(_dbPath))
                         File.Delete(_dbPath);
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"[DB] ClearAll error: {ex.Message}");
               }
          }

          // ─────────────────────────────────────────────────────────────────
          //  MAPPING: Room ↔ RoomRecord
          // ─────────────────────────────────────────────────────────────────
          private static RoomRecord RoomToRecord(Room room)
          {
               return new RoomRecord
               {
                    Name = room.Name,
                    Devices = room.Devices.Select(DeviceToRecord).ToList()
               };
          }

          private static Room RecordToRoom(RoomRecord record)
          {
               var room = new Room(record.Name);
               foreach (var dr in record.Devices)
               {
                    var device = RecordToDevice(dr);
                    if (device != null)
                         room.AddDevice(device);
               }
               return room;
          }

          // ─────────────────────────────────────────────────────────────────
          //  MAPPING: Device ↔ DeviceRecord
          // ─────────────────────────────────────────────────────────────────
          private static DeviceRecord DeviceToRecord(Device device)
          {
               var rec = new DeviceRecord
               {
                    Type = device.GetType().Name,
                    Name = device.Name,
                    Room = device.Room,
                    IsActive = device.IsActive
               };

               if (device is Thermostat t) rec.Temperature = t.Temperature;
               if (device is AirConditioner ac) { rec.Temperature = ac.Temperature; }
               if (device is SmartTV tv) { rec.Channel = tv.CurrentChannel; rec.Volume = tv.Volume; }

               return rec;
          }

          private static Device? RecordToDevice(DeviceRecord rec)
          {
               Device? device = rec.Type switch
               {
                    "Light" => new Light(rec.Room) { Name = rec.Name },
                    "Thermostat" => new Thermostat(rec.Room) { Name = rec.Name, Temperature = rec.Temperature },
                    "DoorLock" => new DoorLock(rec.Room) { Name = rec.Name },
                    "SmartTV" => new SmartTV(rec.Room) { Name = rec.Name },
                    "AirConditioner" => new AirConditioner(rec.Room) { Name = rec.Name, Temperature = rec.Temperature },
                    _ => null
               };

               // Restaurare stare on/off
               if (device != null && rec.IsActive)
                    device.TurnOn();

               return device;
          }
     }
}