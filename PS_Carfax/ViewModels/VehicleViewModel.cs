using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;

namespace PS_Carfax.UI.ViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        public RelayCommand CreateCommand { get; private set; }

        private string _vin;
        public string VIN
        {
            get { return _vin; }
            set
            {
                _vin = value;
                OnPropertyChanged(nameof(VIN));
            }
        }

        private string _make;
        public string Make
        {
            get { return _make; }
            set
            {
                _make = value;
                OnPropertyChanged(nameof(Make));
            }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private int _mileage;
        public int Mileage
        {
            get { return _mileage; }
            set
            {
                _mileage = value;
                OnPropertyChanged(nameof(Mileage));
            }
        }

        private EngineType _engineType;
        public EngineType EngineType
        {
            get { return _engineType; }
            set
            {
                _engineType = value;
                OnPropertyChanged(nameof(EngineType));
            }
        }

        private TransmissionType _transmissionType;
        public TransmissionType TransmissionType
        {
            get { return _transmissionType; }
            set
            {
                _transmissionType = value;
                OnPropertyChanged(nameof(TransmissionType));
            }
        }

        public List<string> EngineTypes
        {
            get { return Enum.GetNames(typeof(EngineType)).ToList(); }
        }

        public List<string> TransmissionTypes
        {
            get { return Enum.GetNames(typeof(TransmissionType)).ToList(); }
        }

        private Vehicle _vehicle;
        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                OnPropertyChanged(nameof(Vehicle));
            }
        }

        private CreateVehicleView _view;
        public VehicleViewModel(CreateVehicleView createVehicleView)
        {
            this._view = createVehicleView;
            Vehicle = new Vehicle();

            CreateCommand = new RelayCommand(Create);
        }

        private void Create(object parameter)
        {
           _view.Close();
        }
    }
}
