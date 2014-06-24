using BigFont.Maps.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace BigFont.Maps.Handlers {
    public class BigFontMap_SingleMarkerHandler : ContentHandler {
        public BigFontMap_SingleMarkerHandler(IRepository<BigFontMap_SingleMarkerRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
