using PS_Carfax.Data.Models;
using PS_Carfax.UI.Properties;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PS_Carfax.UI.ViewModels
{
    public class ShowRecordResultViewModel : ViewModelBase
    {
        private Visibility _vehicleVisibility = Visibility.Collapsed;
        private Visibility _ownerVisibility = Visibility.Collapsed;
        private Visibility _accidentVisibility = Visibility.Collapsed;
        private Visibility _serviceRecordVisibility = Visibility.Collapsed;
        public ICommand SelectHistory { get; private set; }

        public Visibility VehicleVisibility
        {
            get { return _vehicleVisibility; }
            set
            {
                _vehicleVisibility = value;
                OnPropertyChanged("VehicleVisibility");
            }
        }

        public Visibility OwnerVisibility
        {
            get { return _ownerVisibility; }
            set
            {
                _ownerVisibility = value;
                OnPropertyChanged("OwnerVisibility");
            }
        }

        public Visibility AccidentVisibility
        {
            get { return _accidentVisibility; }
            set
            {
                _accidentVisibility = value;
                OnPropertyChanged("AccidentVisibility");
            }
        }

        public Visibility ServiceRecordVisibility
        {
            get { return _serviceRecordVisibility; }
            set
            {
                _serviceRecordVisibility = value;
                OnPropertyChanged("ServiceRecordVisibility");
            }
        }

        private History _selectedHistory;
        public History SelectedHistory
        {
            get { return _selectedHistory; }
            set
            {
                _selectedHistory = value;
                OnPropertyChanged(nameof(SelectedHistory));
                UpdateTableVisibilities();
                UpdateDataTables();
            }
        }

        private void UpdateTableVisibilities()
        {
            if (SelectedHistory != null)
            {
                VehicleVisibility = Visibility.Visible;
                OwnerVisibility = Visibility.Visible;
                AccidentVisibility = Visibility.Visible;
                ServiceRecordVisibility = Visibility.Visible;
            }
            else
            {
                VehicleVisibility = Visibility.Collapsed;
                OwnerVisibility = Visibility.Collapsed;
                AccidentVisibility = Visibility.Collapsed;
                ServiceRecordVisibility = Visibility.Collapsed;
            }
        }

        // Properties for binding
        private ObservableCollection<Vehicle> _vehicles;
        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        private ObservableCollection<Owner> _owners;
        public ObservableCollection<Owner> Owners
        {
            get { return _owners; }
            set
            {
                _owners = value;
                OnPropertyChanged(nameof(Owners));
            }
        }

        private ObservableCollection<Accident> _accidents;
        public ObservableCollection<Accident> Accidents
        {
            get { return _accidents; }
            set
            {
                _accidents = value;
                OnPropertyChanged(nameof(Accidents));
            }
        }

        private ObservableCollection<ServiceRecord> _serviceRecords;
        public ObservableCollection<ServiceRecord> ServiceRecords
        {
            get { return _serviceRecords; }
            set
            {
                _serviceRecords = value;
                OnPropertyChanged(nameof(ServiceRecords));
            }
        }

        private ObservableCollection<History> _histories;
        public ObservableCollection<History> Histories
        {
            get { return _histories; }
            set
            {
                _histories = value;
                OnPropertyChanged(nameof(Histories));
            }
        }

        private void UpdateDataTables()
        {
            var vehicles = new List<Vehicle>();
            vehicles.Add(this.SelectedHistory.Vehicle);
            Vehicles = new ObservableCollection<Vehicle>(vehicles);
            Owners = new ObservableCollection<Owner>(this.SelectedHistory.Owners);
            Accidents = new ObservableCollection<Accident>(this.SelectedHistory.Accidents);
            ServiceRecords = new ObservableCollection<ServiceRecord>(this.SelectedHistory.ServiceRecords);
        }

        public ShowRecordResultViewModel(ICollection<History> histories)
        {
            Histories = new ObservableCollection<History>(histories);
            Vehicles = new ObservableCollection<Vehicle>();
            Owners = new ObservableCollection<Owner>();
            Accidents = new ObservableCollection<Accident>();
            ServiceRecords = new ObservableCollection<ServiceRecord>();

            // No need for RelayCommand here? ask the Teacher
            //SelectHistory = new RelayCommand(UpdateDataTables);
        }
    }
}
