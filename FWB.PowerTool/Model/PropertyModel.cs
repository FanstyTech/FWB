namespace FWB.PowerTool.Model
{
    public class PropertyModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public string MinLength { get; set; }
        public string MaxLength { get; set; }
        public bool IsNullable { get; set; }
        public bool HasAdvancedFilter { get; set; }
        public bool IncludeInCreateUpdate { get; set; }
    }
}
