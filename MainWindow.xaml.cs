using System.Windows;
using SmartHouseApp.Models;
using SmartHouseApp.Managers;

namespace SmartHouseApp
{
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();
          }

          private void Test_Click(object sender, RoutedEventArgs e)
          {
               var manager = new SmartHomeManager();

               var room = new Room("Living Room");
               room.AddDevice(new Light("Living Room"));

               manager.AddRoom(room);

               MessageBox.Show("Room added: " + manager.Rooms.Count);
          }
     }
}