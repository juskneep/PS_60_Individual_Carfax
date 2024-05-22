using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Properties;
using PS_Carfax.UI.Services;
using PS_Carfax.UI.Views;

namespace PS_Carfax.UI.ViewModels
{
    public class SearchRecordViewModel : ViewModelBase
    {
        private readonly DataService _dataService;
        public event EventHandler<HistoryEventArgs> SearchCompleted;
        public History History { get; set; }

        // Properties for binding
        private string? _make;
        public string? Make
        {
            get { return _make; }
            set
            {
                _make = value;
                OnPropertyChanged(nameof(Make));
            }
        }

        private string? _model;
        public string? Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private DateTime _year;
        public DateTime Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private string? _ownerName;
        public string? OwnerName
        {
            get { return _ownerName; }
            set
            {
                _ownerName = value;
                OnPropertyChanged(nameof(OwnerName));
            }
        }

        private FreeTextField _freeTextField = new FreeTextField();
        public FreeTextField FreeTextField
        {
            get { return _freeTextField; }
            set
            {
                _freeTextField = value;
                OnPropertyChanged(nameof(FreeTextField));
            }
        }

        private string _validationMessage;
        public string ValidationMessage
        {
            get { return _validationMessage; }
            set
            {
                _validationMessage = value;
                OnPropertyChanged(nameof(ValidationMessage));
            }
        }

        // Commands
        public ICommand SearchCommand { get; private set; }

        public SearchRecordViewModel(DataService dataService)
        {
            this._dataService = dataService;
            SearchCommand = new RelayCommand(Search);
        }

        private void Search(object parameter)
        {
            var histories = _dataService.SearchHistories(this.Year.Year, this.OwnerName, this.Model, this.Make);
            if(!String.IsNullOrEmpty(this.FreeTextField.FieldName) && !String.IsNullOrEmpty(this.FreeTextField.Value))
                histories = histories.Where(history => MatchFreeTextField(history, this.FreeTextField))
                        .ToList();

            if (!histories.Any(history => history.YearGenerated == this.Year.Year))
            {
                var result = MessageBox.Show($"No records found for the year {this.Year.Year}. Do you want to create a History for this year?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Navigate to the AddHistoryView to create a new History
                    var addHistoryView = new AddToYearlyResultsView();
                    addHistoryView.DataContext = new AddHistoryViewModel(this._dataService, this.Year.Year); // Pass the year to the ViewModel
                    addHistoryView.Show();
                    return;
                }
            }

            var showRecordView = new ShowRecordResultView();
            showRecordView.DataContext = new ShowRecordResultViewModel(histories);
            showRecordView.Show();
        }

        private bool MatchFreeTextField<T>(T entity, FreeTextField field)
        {
            try
            {
                var entityValue = GetEntityValue(entity, field.FieldName);

                string propName;
                string propValue;
                try
                {
                    var data = JsonSerializer.Deserialize<Dictionary<string, string>>(field.Value);
                    var keyValuePair = data.FirstOrDefault();
                    propName = keyValuePair.Key;
                    propValue = keyValuePair.Value;
                }
                catch (Exception ex)
                {
                    ValidationMessage = $"Error parsing FreeTextField '{field.FieldName}': {ex.Message}";
                    return false;
                }

                if (entityValue is string)
                {
                    return entityValue?.ToString() == field.Value?.ToString();
                }
                else if (entityValue is IEnumerable<T>)
                {
                    foreach (var item in (IEnumerable<T>)entityValue)
                    {
                        if (item.GetType().GetProperty(propName) != null)
                        {
                            var itemValue = item.GetType().GetProperty(propName).GetValue(item);

                            if (itemValue?.ToString().ToLower() == propValue?.ToString().ToLower())
                            {
                                return true;
                            }
                        }
                    }
                    ValidationMessage = $"No matching record found in entity (based on property type)";
                    return false;
                }
                else
                {
                    ValidationMessage = $"Unexpected data type returned for field '{field.FieldName}'";
                    return false;
                }
            }
            catch (KeyNotFoundException)
            {
                ValidationMessage = $"Entity doesn't have a field named '{field.FieldName}'";
                return false;
            }
        }

        private object GetEntityValue<T>(T entity, string fieldName)
        {
            var property = entity.GetType().GetProperty(fieldName);
            if (property == null)
            {
                throw new KeyNotFoundException($"Property '{fieldName}' not found on type '{typeof(T).Name}'");
            }
            return property.GetValue(entity);
        }


        private object GetHistoryValue(History history, string fieldName)
        {
            switch (fieldName.ToLower())
            {
                case "id":
                    return history.Id.ToString();
                case "vin":
                    return history.VIN;
                case "yeargenerated":
                    return history.YearGenerated.ToString();
                case "owners":
                    if (history.Owners?.Count > 0)
                    {
                        return history.Owners;
                    }
                    return null;
                case "accidents":
                    if (history.Accidents?.Count > 0)
                    {
                        return history.Accidents;
                    }
                    return null;
                case "servicerecords":
                    if (history.ServiceRecords?.Count > 0)
                    {
                        return history.ServiceRecords;
                    }
                    return null;
                default:
                    return null;
            }
        }

    }

    public class FreeTextField
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
