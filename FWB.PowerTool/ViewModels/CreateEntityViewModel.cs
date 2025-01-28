using FWB.PowerTool.Model;
using FWB.PowerTool.Services;
//using FWB.Shared.Consts;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace FWB.PowerTool.ViewModels
{
    public class CreateEntityViewModel : BaseViewModel
    {
        private string _entityName;
        private string _selectedMapper;
        private ObservableCollection<PropertyModel> _properties;
        private ICommand _addPropertyCommand;
        private ICommand _createEntityCommand;

        public string EntityName
        {
            get => _entityName;
            set => SetProperty(ref _entityName, value);
        }

        public string SelectedMapper
        {
            get => _selectedMapper;
            set => SetProperty(ref _selectedMapper, value);
        }

        public ObservableCollection<PropertyModel> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }

        public ICommand AddPropertyCommand
        {
            get => _addPropertyCommand;
            set => SetProperty(ref _addPropertyCommand, value);
        }

        public ICommand CreateEntityCommand
        {
            get => _createEntityCommand;
            set => SetProperty(ref _createEntityCommand, value);
        }

        public CreateEntityViewModel()
        {
            Properties = new ObservableCollection<PropertyModel>();
            _entityName = string.Empty;
            _selectedMapper = string.Empty;
            _addPropertyCommand = new RelayCommand(ExecuteAddProperty);
            _createEntityCommand = new RelayCommand(ExecuteCreateEntity, CanCreateEntity);
        }

        private void ExecuteAddProperty(object parameter)
        {
            var window = new Views.AddPropertyWindow();
            if (window.ShowDialog() == true)
            {
                Properties.Add(window.Property);
            }
        }

        private void ExecuteCreateEntity(object parameter)
        {
            try
            {
                // Get the output directory path
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "GeneratedCode"
                );

                // Create output directory if it doesn't exist
                Directory.CreateDirectory(outputPath);

                //// Create code generator with AutoMapper as default
                //var codeGenerator = new CodeGenerator(
                //    outputPath, 
                //    EntityName, 
                //    Mappers.AutoMapper, 
                //    Properties
                //);

                //// Generate all the code
                //codeGenerator.GenerateCode();

                // Build the entity information string
                string entityInfo = $@"
Entity Name: {EntityName}
Selected Mapper: {SelectedMapper}
Number of Properties: {Properties.Count}

Properties:
{string.Join(Environment.NewLine, Properties.Select(p => $"- {p.Type} {p.Name} (Required: {p.IsRequired})"))}

Code has been generated in: {outputPath}
The following files were created:
- {EntityName}.cs (Entity)
- {EntityName}ModifyModel.cs (DTO for modifications)
- {EntityName}ReadModel.cs (DTO for reading)
- {EntityName}Validator.cs (FluentValidation)
- {EntityName}Service.cs (Service layer)
- {EntityName}Controller.cs (API Controller)
- EntityConfiguration.cs (Entity Framework configuration)

Press ENTER to exit...";

                // Create a temporary file to store the entity information
                string tempFilePath = Path.Combine(Path.GetTempPath(), "entity_info.txt");
                File.WriteAllText(tempFilePath, entityInfo);

                // Open cmd.exe, display the contents of the temporary file, and close on key press
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/k type \"{tempFilePath}\" && pause && exit",
                    UseShellExecute = true,
                    WorkingDirectory = outputPath
                };

                // Start the process and wait for it to exit
                var process = System.Diagnostics.Process.Start(processInfo);
                process.WaitForExit();

                // Delete the temporary file
                File.Delete(tempFilePath);

                // Exit the application
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error generating code: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private bool CanCreateEntity(object parameter)
        {
            return !string.IsNullOrWhiteSpace(EntityName) && Properties.Count > 0;
        }
    }
}
