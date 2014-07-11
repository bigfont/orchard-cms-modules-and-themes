using BigFont.Bootstrap.Components.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace BigFont.Bootstrap.Components.Handlers
{
    public class ContactUsHandler : ContentHandler {
        public ContactUsHandler(IRepository<ContactUsRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}