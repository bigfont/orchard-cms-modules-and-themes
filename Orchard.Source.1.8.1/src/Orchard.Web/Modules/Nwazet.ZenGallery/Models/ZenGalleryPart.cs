using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Nwazet.ZenGallery.Models {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPart : ContentPart<ZenGalleryPartRecord> {
        public string Path { get { return Record.Path; } set { Record.Path = value; } }
    }
}