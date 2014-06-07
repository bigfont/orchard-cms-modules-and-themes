namespace BigFont.DemoModule.Handlers
{
    using BigFont.DemoModule.Models;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;
    public class DemoModuleHandler : ContentHandler
    {
        public DemoModuleHandler(IRepository<DemoModuleRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}