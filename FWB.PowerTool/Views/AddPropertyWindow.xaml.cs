using System.Windows;
using FWB.PowerTool.Model;
using FWB.PowerTool.ViewModels;

namespace FWB.PowerTool.Views
{
    public partial class AddPropertyWindow : Window
    {
        public PropertyModel Property { get; private set; }
        private readonly AddPropertyViewModel _viewModel;

        public AddPropertyWindow()
        {
            InitializeComponent();
            _viewModel = new AddPropertyViewModel();
            DataContext = _viewModel;
        }

        protected override void OnSourceInitialized(System.EventArgs e)
        {
            base.OnSourceInitialized(e);
            
            _viewModel.SaveCommand = new RelayCommand(_ =>
            {
                Property = new PropertyModel
                {
                    Name = _viewModel.PropertyName,
                    Type = _viewModel.PropertyType,
                    IsRequired = _viewModel.IsRequired
                };
                DialogResult = true;
                Close();
            }, _ => !string.IsNullOrWhiteSpace(_viewModel.PropertyName) && !string.IsNullOrWhiteSpace(_viewModel.PropertyType));

            _viewModel.CancelCommand = new RelayCommand(_ =>
            {
                DialogResult = false;
                Close();
            });
        }
    }
}
