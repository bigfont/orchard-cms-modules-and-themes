using System.Collections.Generic;
using Orchard.Environment.Extensions;
using Orchard.MediaLibrary.Models;

namespace Nwazet.ZenGallery.ViewModels {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPartEditViewModel {
        public string Path { get; set; }
        public string PathPattern { get; set; }
        public IEnumerable<MediaPart> Images { get; set; }
    }
}
