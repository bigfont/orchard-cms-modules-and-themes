using Nwazet.ZenGallery.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Nwazet.ZenGallery.Handlers {
    public class ZenGalleryPartHandler : ContentHandler {
        public ZenGalleryPartHandler(IRepository<ZenGalleryPartRecord> zenGalleryPartRepository) {
            Filters.Add(StorageFilter.For(zenGalleryPartRepository));
        }
    }
}
