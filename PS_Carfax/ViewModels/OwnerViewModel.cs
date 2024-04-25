using System.ComponentModel;
using System.Runtime.CompilerServices;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;

namespace PS_Carfax.UI.ViewModels
{
    public class OwnerViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _contactInfo;
        public string ContactInfo
        {
            get { return _contactInfo; }
            set
            {
                _contactInfo = value;
                OnPropertyChanged(nameof(ContactInfo));
            }
        }

        private Owner _owner;
        public Owner Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        private CreateOwnerView _view;
        public OwnerViewModel(CreateOwnerView view)
        {
            this._view = view;
            Owner = new Owner();
            CreateCommand = new RelayCommand(Create);
        }

        public RelayCommand CreateCommand { get; private set; }
        private void Create(object parameter)
        {
            this._view.Close();
        }
    }
}
