using System.Configuration;
using System.Data;
using System.Windows;
using SmartHouseApp.Services;

namespace SmartHouseApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
          protected override void OnStartup(StartupEventArgs e)
          {
               base.OnStartup(e);

               // Inițializare servicii globale o singură dată
               ServiceLocator.Initialize(
                   database: new DatabaseService(),
                   logger: new LoggerService()
               );

               ServiceLocator.Logger.LogInfo("=== SmartHouse Application Started ===");
          }

          protected override void OnExit(ExitEventArgs e)
          {
               ServiceLocator.Logger.LogInfo("=== SmartHouse Application Closed ===");
               base.OnExit(e);
          }

     }

}
