using SmartHouseApp.ViewModels;
using SmartHouseApp.Models;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
    public partial class RoomDetailsView : UserControl
    {
        public RoomDetailsView(Room room)
        {
            InitializeComponent();
            DataContext = new RoomDetailsViewModel(room);
        }
    }
}
