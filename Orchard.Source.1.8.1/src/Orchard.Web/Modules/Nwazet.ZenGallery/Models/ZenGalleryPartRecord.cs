using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace Nwazet.ZenGallery.Models {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPartRecord : ContentPartRecord {
        public virtual string Path { get; set; }
    }
}