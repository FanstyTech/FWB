using FWB.PowerTool.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FWB.PowerTool.ViewModels
{
    public class AddPropertyViewModel : BaseViewModel
    {
        private string _propertyName;
        private string _propertyType;
        private bool _isRequired;
        private ICommand _saveCommand;
        private ICommand _cancelCommand;
        private ObservableCollection<string> _propertyTypes;

        public string PropertyName
        {
            get => _propertyName;
            set
            {
                if (SetProperty(ref _propertyName, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string PropertyType
        {
            get => _propertyType;
            set
            {
                if (SetProperty(ref _propertyType, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ObservableCollection<string> PropertyTypes
        {
            get => _propertyTypes;
            set => SetProperty(ref _propertyTypes, value);
        }

        public bool IsRequired
        {
            get => _isRequired;
            set => SetProperty(ref _isRequired, value);
        }

        public ICommand SaveCommand
        {
            get => _saveCommand;
            set => SetProperty(ref _saveCommand, value);
        }

        public ICommand CancelCommand
        {
            get => _cancelCommand;
            set => SetProperty(ref _cancelCommand, value);
        }

        public AddPropertyViewModel()
        {
            _propertyName = string.Empty;
            _propertyType = string.Empty;
            PropertyTypes = new ObservableCollection<string>
            {
                "string",
                "int",
                "long",
                "decimal",
                "double",
                "bool",
                "DateTime",
                "Guid",
                "byte[]",
                "TimeSpan"
            };
            _propertyType = PropertyTypes[0]; // Default to string
            _saveCommand = new RelayCommand(ExecuteSave, CanSave);
            _cancelCommand = new RelayCommand(ExecuteCancel);
        }

        private bool CanSave(object parameter)
        {
            return !string.IsNullOrWhiteSpace(PropertyName) && !string.IsNullOrWhiteSpace(PropertyType);
        }

        private void ExecuteSave(object parameter)
        {
            // Create and return the property
            var property = new PropertyModel
            {
                Name = PropertyName,
                Type = PropertyType,
                IsRequired = IsRequired
            };

            // The window that owns this ViewModel should handle the DialogResult
        }

        private void ExecuteCancel(object parameter)
        {
            // The window that owns this ViewModel should handle closing
        }
    }
}
