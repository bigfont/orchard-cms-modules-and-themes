namespace BigFont.DemoModule2.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Records;

    public class DemoModule2Record : ContentPartRecord
    {
        public virtual string SomePropName { get; set; }
    }

    public class DemoModule2Part : ContentPart<DemoModule2Record>
    {
        [Required]
        public string SomePropName
        {
            get { return Record.SomePropName; }
            set { Record.SomePropName = value; }
        }
    }
}