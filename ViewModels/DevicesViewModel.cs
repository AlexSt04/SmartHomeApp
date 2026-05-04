using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
    public class DevicesViewModel : ViewModelBase
    {
        private readonly SmartHomeFacade _facade;
        private ObservableCollection<DeviceViewModel> _allDevices;
        
        public ObservableCollection<DeviceViewModel> AllDevices
        {
            get => _allDevices;
            set => SetProperty(ref _allDevices, value);
        }
        
        public ICommand RefreshCommand { get; }
        public ICommand ToggleAllCommand { get; }
        
        public DevicesViewModel()
        {
            _facade = new SmartHomeFacade();
            RefreshCommand = new RelayCommand(LoadDevices);
            ToggleAllCommand = new RelayCommand(ToggleAll);
            
            LoadDevices();
        }
        
        private void LoadDevices()
        {
            var devices = new ObservableCollection<DeviceViewModel>();
            
            foreach (var room in SmartHomeManager.Instance.Rooms)
            {
                foreach (var device in room.Devices)
                {
                    devices.Add(new DeviceViewModel(device));
                }
            }
            
            AllDevices = devices;
        }
        
        private void ToggleAll()
        {
            if (AllDevices == null) return;
            foreach (var device in AllDevices)
            {
                device.ToggleCommand.Execute(null);
            }
        }
    }
}
