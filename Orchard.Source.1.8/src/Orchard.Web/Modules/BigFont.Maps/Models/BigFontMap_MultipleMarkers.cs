using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace BigFont.Maps.Models {
    public class BigFontMap_MultipleMarkersRecord : ContentPartRecord {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
    }

    public class BigFontMap_MultipleMarkersPart : ContentPart<BigFontMap_MultipleMarkersRecord> {
        [Required]
        public double Latitude {
            get { return Record.Latitude; }
            set { Record.Latitude = value; }
        }

        [Required]
        public double Longitude {
            get { return Record.Longitude; }
            set { Record.Longitude = value; }
        }
    }
}
