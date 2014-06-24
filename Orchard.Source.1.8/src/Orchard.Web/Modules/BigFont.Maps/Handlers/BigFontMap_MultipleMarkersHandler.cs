using BigFont.Maps.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace BigFont.Maps.Handlers {
    public class BigFontMap_MultipleMarkersHandler : ContentHandler {
        public BigFontMap_MultipleMarkersHandler(IRepository<BigFontMap_MultipleMarkersRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
