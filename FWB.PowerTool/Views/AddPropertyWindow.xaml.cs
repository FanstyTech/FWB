using System.Windows;

namespace FWB.PowerTool.Views
{
    public partial class AddPropertyWindow : Window
    {
        public PropertyInfo PropertyResult { get; private set; }

        public AddPropertyWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowInTaskbar = false;
            ResizeMode = ResizeMode.NoResize;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(PropertyNameTextBox.Text))
            {
                MessageBox.Show("Property Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create property info object
            PropertyResult = new PropertyInfo
            {
                PropertyType = PropertyTypeComboBox.Text,
                PropertyName = PropertyNameTextBox.Text,
                MinLength = MinLengthTextBox.Text,
                MaxLength = MaxLengthTextBox.Text,
                RegularExpression = RegularExpressionTextBox.Text,
                Required = RequiredCheckBox.IsChecked ?? false,
                Nullable = NullableCheckBox.IsChecked ?? false,
                MultiLingualProperty = MultiLingualPropertyCheckBox.IsChecked ?? false,
                AdvancedFilter = AdvancedFilterCheckBox.IsChecked ?? false,
                ListInUI = ListInUICheckBox.IsChecked ?? false,
                ListInMaui = ListInMauiCheckBox.IsChecked ?? false,
                CreateAndUpdate = CreateAndUpdateCheckBox.IsChecked ?? false
            };

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void PropertyTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }

    public class PropertyInfo
    {
        public string PropertyType { get; set; }
        public string PropertyName { get; set; }
        public string MinLength { get; set; }
        public string MaxLength { get; set; }
        public string RegularExpression { get; set; }
        public bool Required { get; set; }
        public bool Nullable { get; set; }
        public bool MultiLingualProperty { get; set; }
        public bool AdvancedFilter { get; set; }
        public bool ListInUI { get; set; }
        public bool ListInMaui { get; set; }
        public bool CreateAndUpdate { get; set; }
    }
}
