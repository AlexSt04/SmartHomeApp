using SmartHouseApp.ViewModels;
using System.Windows.Controls;

namespace SmartHouseApp.Views.Pages
{
    public partial class RoomsView : UserControl
    {
        public RoomsView()
        {
            InitializeComponent();
            DataContext = new RoomsViewModel();
        }
    }
}
