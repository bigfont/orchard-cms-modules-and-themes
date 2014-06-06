using Contrib.RecentContent.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Contrib.RecentContent.Handlers {
    public class RecentContentWidgetPartHandler : ContentHandler {
        public RecentContentWidgetPartHandler(IRepository<RecentContentWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}