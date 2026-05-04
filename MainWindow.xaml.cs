using System.Windows;
using SmartHouseApp.Managers;
using SmartHouseApp.Builders;
using SmartHouseApp.Services;
using SmartHouseApp.Views.Pages;

namespace SmartHouseApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NavigationService.Instance.Initialize(MainContent, PageTitle);

            // Setup default rooms for demo if empty
            if (SmartHomeManager.Instance.Rooms.Count == 0)
            {
                var builder = new RoomBuilder();
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Living Room").AddLight().AddThermostat().Build());
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Kitchen").AddLight().Build());
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Entrance").AddDoorLock().Build());
            }

            // Default page
            NavigationService.Instance.NavigateTo(new DashboardView(), "Dashboard");
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Instance.NavigateTo(new DashboardView(), "Dashboard");
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Instance.NavigateTo(new RoomsView(), "Rooms");
        }

        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Instance.NavigateTo(new DevicesView(), "Devices");
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logs system is active and writing to the Logs/ folder.", "Information");
        }

        private void Settings_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Instance.NavigateTo(new SettingsView(), "Settings");
        }
    }
}