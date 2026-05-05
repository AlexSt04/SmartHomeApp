using SmartHouseApp.Managers;
using SmartHouseApp.Models;
using SmartHouseApp.Services;
using SmartHouseApp.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SmartHouseApp.ViewModels
{
     public class RoomDetailsViewModel : ViewModelBase
     {
          private readonly Room _room;
          private ObservableCollection<DeviceViewModel> _devices = new();

          public string Name => _room.Name;

          public ObservableCollection<DeviceViewModel> Devices
          {
               get => _devices;
               set => SetProperty(ref _devices, value);
          }

          public ICommand AddDeviceCommand { get; }
          public ICommand DeleteDeviceCommand { get; }
          public ICommand BackCommand { get; }

          private string _newDeviceName = "My Device";
          public string NewDeviceName
          {
               get => _newDeviceName;
               set => SetProperty(ref _newDeviceName, value);
          }

          public List<string> DeviceTypes { get; } = new()
        {
            "Light", "Thermostat", "Door Lock", "Smart TV", "Air Conditioner"
        };

          private string _selectedDeviceType = "Light";
          public string SelectedDeviceType
          {
               get => _selectedDeviceType;
               set => SetProperty(ref _selectedDeviceType, value);
          }

          public RoomDetailsViewModel(Room room)
          {
               _room = room;
               LoadDevices();

               AddDeviceCommand = new RelayCommand(AddDeviceAction);
               DeleteDeviceCommand = new RelayCommand<DeviceViewModel>(DeleteDevice);
               BackCommand = new RelayCommand(() =>
                   NavigationService.Instance.NavigateTo(new Views.Pages.DashboardView(), "Dashboard"));
          }

          private void LoadDevices()
          {
               Devices = new ObservableCollection<DeviceViewModel>(
                   _room.Devices.Select(d => new DeviceViewModel(d))
               );
          }

          private void AddDeviceAction()
          {
               Device? newDevice = SelectedDeviceType switch
               {
                    "Light" => new Light(_room.Name) { Name = NewDeviceName },
                    "Thermostat" => new Thermostat(_room.Name) { Name = NewDeviceName },
                    "Door Lock" => new DoorLock(_room.Name) { Name = NewDeviceName },
                    "Smart TV" => new SmartTV(_room.Name) { Name = NewDeviceName },
                    "Air Conditioner" => new AirConditioner(_room.Name) { Name = NewDeviceName },
                    _ => null
               };

               if (newDevice != null)
               {
                    _room.AddDevice(newDevice);

                    // ✅ Salvează camera actualizată în DB
                    ServiceLocator.Database.UpsertRoom(_room);
                    ServiceLocator.Logger.LogDeviceAction(newDevice.Name, _room.Name, "Device added to room");

                    NewDeviceName = "My Device";
                    LoadDevices();
               }
          }

          private void DeleteDevice(DeviceViewModel deviceVm)
          {
               var device = _room.Devices.FirstOrDefault(d => d.Name == deviceVm.Name);
               if (device != null)
               {
                    _room.Devices.Remove(device);

                    // ✅ Salvează camera actualizată în DB
                    ServiceLocator.Database.UpsertRoom(_room);
                    ServiceLocator.Logger.LogDeviceAction(device.Name, _room.Name, "Device removed from room");

                    LoadDevices();
               }
          }
     }
}