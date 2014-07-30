using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using LccNetwork.Models;
using Orchard.Environment.Extensions;

namespace LccNetwork.Handlers
{
    [OrchardFeature("Highlightable")]
    public class HighlightedItemPartHandler : ContentHandler {
        public HighlightedItemPartHandler(IRepository<HighlightedItemPartRecord> projecRepository)
        {
            Filters.Add(StorageFilter.For(projecRepository));
        }
    
    }
}