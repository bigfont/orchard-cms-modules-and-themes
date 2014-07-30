using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using LccNetwork.Models;
using Orchard.Environment.Extensions;

namespace LccNetwork.Handlers
{
    [OrchardFeature("Highlightable")]
    public class HighlightableItemPartHandler : ContentHandler {

        private readonly IRepository<HighlightableItemPartRecord> _highlightableItemPartRepository;
        public HighlightableItemPartHandler(
            IRepository<HighlightableItemPartRecord> projectRepository)
        {
            _highlightableItemPartRepository = projectRepository;

            Filters.Add(StorageFilter.For(projectRepository));

            OnUpdated<HighlightableItemPart>((ctx, part) => EnsureThereIsOnlyOneHighlightedPart(ctx, part));            
        }

        private void EnsureThereIsOnlyOneHighlightedPart(UpdateContentContext ctx, HighlightableItemPart part)
        {
            if (part.IsHighlighted)
            {
                var items = _highlightableItemPartRepository.Fetch(h => h.Id != part.Id && h.HighlightGroup.Equals(part.HighlightGroup));
                foreach (var item in items)
                {
                    _highlightableItemPartRepository.Delete(item);
                    _highlightableItemPartRepository.Flush();
                }
            }                        
        }
    }
}