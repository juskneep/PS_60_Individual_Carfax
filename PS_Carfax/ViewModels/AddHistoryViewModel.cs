using System.Collections.ObjectModel;
using System.Windows.Input;
using PS_Carfax.Data.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PS_Carfax.UI.Views;
using PS_Carfax.UI.Services;

namespace PS_Carfax.UI.ViewModels
{
    public class AddHistoryViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        private History _history;
        public History History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged(nameof(History));
            }
        }

        public ObservableCollection<Owner> Owners { get; set; }
        public ObservableCollection<Accident> Accidents { get; set; }
        public ObservableCollection<ServiceRecord> ServiceRecords { get; set; }

        public AddHistoryViewModel(DataService dataService, int yearGenerated)
        {
            this._dataService = dataService;
            History = new History { YearGenerated = yearGenerated };
            Owners = new ObservableCollection<Owner>();
            Accidents = new ObservableCollection<Accident>();
            ServiceRecords = new ObservableCollection<ServiceRecord>();
        }

        public ICommand AddOwnerCommand => new RelayCommand(AddOwner);
        public ICommand AddAccidentCommand => new RelayCommand(AddAccident);
        public ICommand AddServiceRecordCommand => new RelayCommand(AddServiceRecord);
        public ICommand AddVehicleCommand => new RelayCommand(AddVehicle);
        public ICommand CreateCommand => new RelayCommand(CreateHistory);

        private void AddOwner(object parameter)
        {
            CreateOwnerView ownerView = new CreateOwnerView();
            ownerView.ShowDialog();

            if (ownerView.DataContext is OwnerViewModel ownerViewModel && ownerViewModel.Owner != null)
            {
                Owners.Add(ownerViewModel.Owner);
                History.AddOwner(ownerViewModel.Owner);
            }
        }

        private void AddAccident(object parameter)
        {
            CreateAccidentView accidentView = new CreateAccidentView();
            accidentView.ShowDialog();

            if (accidentView.DataContext is AccidentViewModel accidentViewModel && accidentViewModel.Accident != null)
            {
                Accidents.Add(accidentViewModel.Accident);
                History.AddAccident(accidentViewModel.Accident);
            }
        }

        private void AddServiceRecord(object parameter)
        {
            ServiceRecordView serviceRecordView = new ServiceRecordView();
            serviceRecordView.ShowDialog();

            if (serviceRecordView.DataContext is ServiceRecordViewModel serviceRecordViewModel && serviceRecordViewModel.ServiceRecord != null)
            {
                ServiceRecords.Add(serviceRecordViewModel.ServiceRecord);
                History.AddServiceRecord(serviceRecordViewModel.ServiceRecord);
            }
        }

        private void AddVehicle(object parameter)
        {
            CreateVehicleView vehicleView = new CreateVehicleView();
            vehicleView.ShowDialog();

            if (vehicleView.DataContext is VehicleViewModel vehicleViewModel && vehicleViewModel.Vehicle != null)
            {
                History.Vehicle = vehicleViewModel.Vehicle;
            }
        }

        public void CreateHistory(object parameters)
        {
            // Add Owners
            foreach (var owner in Owners)
            {
                History.AddOwner(owner);
            }

            // Add Accidents
            foreach (var accident in Accidents)
            {
                History.AddAccident(accident);
            }

            // Add Service Records
            foreach (var serviceRecord in ServiceRecords)
            {
                History.AddServiceRecord(serviceRecord);
            }

            // Save History to database
            this._dataService.CreateHistory(History);

            // Clear collections
            Owners.Clear();
            Accidents.Clear();
            ServiceRecords.Clear();
        }
    }
}