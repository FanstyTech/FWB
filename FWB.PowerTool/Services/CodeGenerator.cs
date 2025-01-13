using FWB.PowerTool.Model;
using System.Collections.Generic;
using System.IO;

namespace FWB.PowerTool.Services
{
    public class CodeGenerator
    {
        private readonly string _outputPath;
        private readonly string _entityName;
        private readonly string _mapper;
        private readonly IEnumerable<PropertyModel> _properties;

        public CodeGenerator(string outputPath, string entityName, string mapper, IEnumerable<PropertyModel> properties)
        {
            _outputPath = outputPath;
            _entityName = entityName;
            _mapper = mapper;
            _properties = properties;
        }

        public void GenerateCode()
        {
            // Create an EntityBuilder instance
            var builder = EntityBuilder.New(_mapper, _entityName, PkTypes.GUID);

            // Set the solution path to our output directory
            builder.SolutionPath = _outputPath;

            // Generate all the necessary files
            builder.GenerateModels()  // Generates ModifyModel and ReadModel
                  .GenerateEntity()   // Generates the main entity
                  .GenerateValidator() // Generates validator
                  .GenerateService()  // Generates service layer
                  .GenerateController(); // Generates API controller

            // If using AutoMapper, it will automatically generate the mapping profile
            if (_mapper == Mappers.AutoMapper)
            {
                builder.BuildAutoMapperProfile();
            }

            // Generate entity configuration
            builder.GenerateEntityConfig();

            // Update DbContext with the new entity
            builder.DbContextMapping();
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
