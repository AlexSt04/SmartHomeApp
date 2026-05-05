namespace SmartHouseApp.Services
{
     /// <summary>
     /// ServiceLocator simplu — injectat o singură dată în App.xaml.cs,
     /// accesibil global din orice ViewModel sau Manager.
     /// </summary>
     public static class ServiceLocator
     {
          public static IDatabaseService Database { get; private set; } = null!;
          public static ILoggerService Logger { get; private set; } = null!;

          public static void Initialize(IDatabaseService database, ILoggerService logger)
          {
               Database = database;
               Logger = logger;
          }
     }
}