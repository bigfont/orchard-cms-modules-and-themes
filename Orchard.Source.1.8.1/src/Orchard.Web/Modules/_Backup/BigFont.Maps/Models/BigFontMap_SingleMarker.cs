using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace BigFont.Maps.Models
{
    public class BigFontMap_SingleMarkerRecord : ContentPartRecord
    {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual string TextDirections { get; set; }
    }

    public class BigFontMap_SingleMarkerPart : ContentPart<BigFontMap_SingleMarkerRecord>
    {
        [Required]
        public double Latitude
        {
            get { return Record.Latitude; }
            set { Record.Latitude = value; }
        }

        [Required]
        public double Longitude
        {
            get { return Record.Longitude; }
            set { Record.Longitude = value; }
        }

        [Required]
        public string TextDirections
        {
            get { return Record.TextDirections; }
            set { Record.TextDirections = value; }
        }
    }
}
