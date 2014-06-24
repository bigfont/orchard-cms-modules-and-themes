using BigFont.ContactUs.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace BigFont.ContactUs.Handlers {
    public class BigFontContactUsHandler : ContentHandler {
        public BigFontContactUsHandler(IRepository<BigFontContactUsRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}