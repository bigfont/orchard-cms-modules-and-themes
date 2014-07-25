using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using LccNetwork.Models;

namespace LccNetwork.Handlers
{
    public class HighlightedItemPartHandler : ContentHandler {
        public HighlightedItemPartHandler(IRepository<HighlightedItemPartRecord> projecRepository)
        {
            Filters.Add(StorageFilter.For(projecRepository));
        }
    
    }
}