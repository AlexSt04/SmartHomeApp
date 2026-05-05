using SmartHouseApp.Services;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
     public class LogEntry
     {
          public string Timestamp { get; set; } = "";
          public string Level { get; set; } = "";
          public string Message { get; set; } = "";

          /// <summary>Culoare afișată pe baza nivelului de log.</summary>
          public string LevelColor => Level switch
          {
               "INFO" => "#10B981",
               "WARN" => "#F59E0B",
               "ERROR" => "#EF4444",
               "DEVICE" => "#3B82F6",
               "ROOM" => "#8B5CF6",
               _ => "#A0A0B0"
          };

          public string LevelBackground => Level switch
          {
               "INFO" => "#0D2E22",
               "WARN" => "#2E1F05",
               "ERROR" => "#2E0D0D",
               "DEVICE" => "#0D1A2E",
               "ROOM" => "#1A0D2E",
               _ => "#1E1E2E"
          };
     }

     public class LogsViewModel : ViewModelBase
     {
          private List<LogEntry> _allEntries = new();
          private ObservableCollection<LogEntry> _filteredEntries = new();
          public ObservableCollection<LogEntry> FilteredEntries
          {
               get => _filteredEntries;
               set => SetProperty(ref _filteredEntries, value);
          }

          private string _searchText = "";
          public string SearchText
          {
               get => _searchText;
               set
               {
                    SetProperty(ref _searchText, value);
                    ApplyFilters();
               }
          }

          private string _selectedLevel = "All";
          public string SelectedLevel
          {
               get => _selectedLevel;
               set
               {
                    SetProperty(ref _selectedLevel, value);
                    ApplyFilters();
               }
          }

          public List<string> Levels { get; } = new() { "All", "INFO", "WARN", "ERROR", "DEVICE", "ROOM" };

          public int TotalCount => _allEntries.Count;
          public int ErrorCount => _allEntries.Count(e => e.Level == "ERROR");
          public int WarningCount => _allEntries.Count(e => e.Level == "WARN");
          public int DeviceCount => _allEntries.Count(e => e.Level == "DEVICE");

          public string LogFilePath { get; private set; } = "";

          public ICommand RefreshCommand { get; }
          public ICommand ClearLogsCommand { get; }
          public ICommand ExportCommand { get; }

          public LogsViewModel()
          {
               RefreshCommand = new RelayCommand(LoadLogs);
               ClearLogsCommand = new RelayCommand(ClearLogs);
               ExportCommand = new RelayCommand(ExportLogs);
               LoadLogs();
          }

          private void LoadLogs()
          {
               _allEntries.Clear();

               try
               {
                    string logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (!Directory.Exists(logsDir))
                    {
                         ApplyFilters();
                         return;
                    }

                    // Citește toate fișierele de log, sortat descrescător (cel mai recent primul)
                    var logFiles = Directory.GetFiles(logsDir, "*.log")
                                            .OrderByDescending(f => f)
                                            .Take(5)  // ultimele 5 zile
                                            .ToList();

                    LogFilePath = logFiles.FirstOrDefault() ?? "";

                    foreach (var file in logFiles)
                    {
                         // Folosește FileShare.ReadWrite pentru a citi chiar dacă fișierul e deschis
                         using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                         using var reader = new StreamReader(stream);

                         string? line;
                         while ((line = reader.ReadLine()) != null)
                         {
                              var entry = ParseLogLine(line);
                              if (entry != null)
                                   _allEntries.Add(entry);
                         }
                    }

                    // Cel mai recent sus
                    _allEntries = _allEntries.AsEnumerable().Reverse().ToList();
               }
               catch (Exception ex)
               {
                    _allEntries.Add(new LogEntry
                    {
                         Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                         Level = "ERROR",
                         Message = $"Failed to read logs: {ex.Message}"
                    });
               }

               OnPropertyChanged(nameof(TotalCount));
               OnPropertyChanged(nameof(ErrorCount));
               OnPropertyChanged(nameof(WarningCount));
               OnPropertyChanged(nameof(DeviceCount));
               ApplyFilters();
          }

          private void ApplyFilters()
          {
               var filtered = _allEntries.AsEnumerable();

               if (SelectedLevel != "All")
                    filtered = filtered.Where(e => e.Level == SelectedLevel);

               if (!string.IsNullOrWhiteSpace(SearchText))
                    filtered = filtered.Where(e =>
                        e.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        e.Timestamp.Contains(SearchText));

               FilteredEntries = new ObservableCollection<LogEntry>(filtered.Take(500)); // max 500 linii afișate
          }

          private void ClearLogs()
          {
               try
               {
                    string logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                    if (Directory.Exists(logsDir))
                    {
                         foreach (var f in Directory.GetFiles(logsDir, "*.log"))
                              File.Delete(f);
                    }
                    _allEntries.Clear();
                    ApplyFilters();
                    ServiceLocator.Logger.LogInfo("Logs cleared by user.");
               }
               catch (Exception ex)
               {
                    System.Windows.MessageBox.Show($"Could not clear logs: {ex.Message}", "Error");
               }
          }

          private void ExportLogs()
          {
               try
               {
                    string exportPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        $"SmartHouse_Logs_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
                    );

                    var lines = _allEntries.Select(e => $"[{e.Timestamp}] [{e.Level,-6}] {e.Message}");
                    File.WriteAllLines(exportPath, lines);

                    System.Windows.MessageBox.Show(
                        $"Logs exported successfully!\n\nFile: {exportPath}",
                        "Export Complete",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information
                    );
               }
               catch (Exception ex)
               {
                    System.Windows.MessageBox.Show($"Export failed: {ex.Message}", "Error");
               }
          }

          /// <summary>
          /// Parsează o linie de format: [yyyy-MM-dd HH:mm:ss] [LEVEL ] Message
          /// </summary>
          private static LogEntry? ParseLogLine(string line)
          {
               if (string.IsNullOrWhiteSpace(line)) return null;

               try
               {
                    // Format: [2025-01-01 12:00:00] [INFO  ] Message here
                    if (line.Length < 25) return null;

                    string timestamp = line.Substring(1, 19);   // yyyy-MM-dd HH:mm:ss
                    int levelStart = line.IndexOf('[', 1);
                    int levelEnd = line.IndexOf(']', levelStart + 1);

                    if (levelStart < 0 || levelEnd < 0) return null;

                    string level = line.Substring(levelStart + 1, levelEnd - levelStart - 1).Trim();
                    string message = line.Substring(levelEnd + 2).Trim();

                    return new LogEntry
                    {
                         Timestamp = timestamp,
                         Level = level,
                         Message = message
                    };
               }
               catch
               {
                    return new LogEntry
                    {
                         Timestamp = "",
                         Level = "INFO",
                         Message = line
                    };
               }
          }
     }
}