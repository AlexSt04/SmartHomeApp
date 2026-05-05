using SmartHouseApp.Models;

namespace SmartHouseApp.Services
{
     public interface IDatabaseService
     {
          /// <summary>Încarcă toate camerele și device-urile din DB la pornirea aplicației.</summary>
          List<Room> LoadRooms();

          /// <summary>Salvează TOATE camerele (snapshot complet). Apelat după orice modificare.</summary>
          void SaveAllRooms(List<Room> rooms);

          /// <summary>Adaugă sau actualizează o singură cameră.</summary>
          void UpsertRoom(Room room);

          /// <summary>Șterge o cameră după nume.</summary>
          void DeleteRoom(string roomName);

          /// <summary>Șterge toată baza de date (reset).</summary>
          void ClearAll();

          /// <summary>Returnează calea fișierului DB pentru afișare în Settings.</summary>
          string GetDatabasePath();
     }
}