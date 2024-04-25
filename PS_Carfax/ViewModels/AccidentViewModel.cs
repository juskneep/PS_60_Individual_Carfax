using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;

namespace PS_Carfax.UI.ViewModels
{
    public class AccidentViewModel : ViewModelBase
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

        private string _severity;
        public string Severity
        {
            get { return _severity; }
            set
            {
                _severity = value;
                OnPropertyChanged(nameof(Severity));
            }
        }

        private Accident _accident;
        public Accident Accident
        {
            get { return _accident; }
            set
            {
                _accident = value;
                OnPropertyChanged(nameof(Accident));
            }
        }

        CreateAccidentView _view;
        public AccidentViewModel(CreateAccidentView view)
        {
            Accident = new Accident();
            CreateCommand = new RelayCommand(Create);
            _view = view;
        }

        public RelayCommand CreateCommand { get; private set; }
        private void Create(object parameter)
        {
            this._view.Close();
        }
    }
}
