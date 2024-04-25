using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;

namespace PS_Carfax.UI.ViewModels
{
    public class ServiceRecordViewModel : ViewModelBase
    {
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private int _mileageAtService;
        public int MileageAtService
        {
            get { return _mileageAtService; }
            set
            {
                _mileageAtService = value;
                OnPropertyChanged(nameof(MileageAtService));
            }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private ServiceRecord _serviceRecord;
        public ServiceRecord ServiceRecord
        {
            get { return _serviceRecord; }
            set
            {
                _serviceRecord = value;
                OnPropertyChanged(nameof(ServiceRecord));
            }
        }

        ServiceRecordView _view;
        public ServiceRecordViewModel(ServiceRecordView view)
        {
            this._view = view;
            ServiceRecord = new ServiceRecord();
            CreateCommand = new RelayCommand(Create);
        }

        public RelayCommand CreateCommand { get; private set; }
        private void Create(object parameter)
        {
            _view.Close();
        }
    }
}
