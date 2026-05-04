using System.Windows;
using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Builders;
using SmartHouseApp.Patterns.Flyweight;
using SmartHouseApp.Patterns.Decorator;
using SmartHouseApp.Patterns.Proxy;
using SmartHouseApp.Views.Pages;

namespace SmartHouseApp
{
    public partial class MainWindow : Window
    {
        private readonly SmartHomeFacade facade;

        public MainWindow()
        {
            InitializeComponent();

            facade = new SmartHomeFacade();

            // Setup default rooms for demo if empty
            if (SmartHomeManager.Instance.Rooms.Count == 0)
            {
                var builder = new RoomBuilder();
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Living Room").AddLight().AddThermostat().Build());
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Kitchen").AddLight().Build());
                SmartHomeManager.Instance.AddRoom(builder.CreateRoom("Entrance").AddDoorLock().Build());
            }

            // Default page
            NavigateTo(new DashboardView(), "Dashboard");
        }

        private void NavigateTo(UIElement page, string title)
        {
            MainContent.Content = page;
            if (PageTitle != null) PageTitle.Text = title;
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new DashboardView(), "Dashboard");
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new RoomsView(), "Rooms");
        }

        private void Devices_Click(object sender, RoutedEventArgs e)
        {
            NavigateTo(new DevicesView(), "Devices");
        }

        private void Logs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logs system is active and writing to the Logs/ folder.", "Information");
        }
    }
}