using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using LccNetwork.Models;

namespace LccNetwork.Handlers
{
    public class HighlightableItemPartHandler : ContentHandler {
        public HighlightableItemPartHandler(IRepository<HighlightableItemPartRecord> projecRepository)
        {
            Filters.Add(StorageFilter.For(projecRepository));
        }
    }
}