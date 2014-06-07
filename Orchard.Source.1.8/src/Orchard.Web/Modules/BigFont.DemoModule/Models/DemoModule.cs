namespace BigFont.DemoModule.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Records;

    public class DemoModuleRecord : ContentPartRecord
    {
        public virtual string SomePropName { get; set; }
    }

    public class DemoModulePart : ContentPart<DemoModuleRecord>
    {
        [Required]
        public string SomePropName
        {
            get { return Record.SomePropName; }
            set { Record.SomePropName = value; }
        }
    }
}