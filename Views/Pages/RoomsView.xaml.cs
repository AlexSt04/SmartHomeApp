using SmartHouseApp.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHouseApp.Views.Pages
{
     /// <summary>
     /// Interaction logic for RoomsView.xaml
     /// </summary>
     public partial class RoomsView : UserControl
     {
          SmartHomeFacade facade = new SmartHomeFacade();

          public RoomsView()
          {
               InitializeComponent();
               LoadRooms();
          }

          private void LoadRooms()
          {
               RoomsList.Items.Clear();

               foreach (var room in SmartHomeManager.Instance.Rooms)
               {
                    RoomsList.Items.Add(room.Name);
               }
          }

          private void AddRoom_Click(object sender, RoutedEventArgs e)
          {
               facade.AddRoom("Room " + (SmartHomeManager.Instance.Rooms.Count + 1));
               LoadRooms();
          }
     }
}
