using FWB.PowerTool.ViewModels;
using System.Windows;

namespace FWB.PowerTool.Views
{
    public partial class CreateEntityControl : Window
    {
        private readonly CreateEntityViewModel _viewModel;

        public CreateEntityControl()
        {
            InitializeComponent();
            _viewModel = new CreateEntityViewModel();
            DataContext = _viewModel;
        }


    }
}