using BigFont.Bootstrap.Components.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;

namespace BigFont.Bootstrap.Components.Handlers
{
    [OrchardFeature("Bootstrap.ContactUs")]
    public class ContactUsHandler : ContentHandler {
        public ContactUsHandler(IRepository<ContactUsRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}