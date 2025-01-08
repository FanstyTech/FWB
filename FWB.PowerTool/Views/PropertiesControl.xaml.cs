using System.Windows.Controls;
using System.Windows;

namespace FWB.PowerTool.Views
{
    public partial class PropertiesControl : UserControl
    {
        public PropertiesControl()
        {
            InitializeComponent();
        }

        private void AddPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var window = new AddPropertyWindow();
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    window.Owner = parentWindow;
                }
                
                if (window.ShowDialog() == true)
                {
                    var propertyInfo = window.PropertyResult;
                    // Add the property to the list
                    var propertyPanel = new Grid();
                    propertyPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    propertyPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    var typeTextBlock = new TextBlock
                    {
                        Text = propertyInfo.PropertyType,
                        Foreground = System.Windows.Media.Brushes.White,
                        Margin = new Thickness(20, 10, 0, 10),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    var nameTextBlock = new TextBlock
                    {
                        Text = propertyInfo.PropertyName,
                        Foreground = System.Windows.Media.Brushes.White,
                        Margin = new Thickness(20, 10, 0, 10),
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Grid.SetColumn(typeTextBlock, 0);
                    Grid.SetColumn(nameTextBlock, 1);

                    propertyPanel.Children.Add(typeTextBlock);
                    propertyPanel.Children.Add(nameTextBlock);

                    PropertiesList.Items.Add(propertyPanel);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error opening property window: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
