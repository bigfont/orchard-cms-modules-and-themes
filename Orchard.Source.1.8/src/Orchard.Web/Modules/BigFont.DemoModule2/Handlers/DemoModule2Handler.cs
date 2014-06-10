namespace BigFont.DemoModule2.Handlers
{
    using BigFont.DemoModule2.Models;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;
    public class DemoModule2Handler : ContentHandler
    {
        public DemoModule2Handler(IRepository<DemoModule2Record> repository)
        {
            Filters.Add(StorageFilter.For(repository));

            OnActivated<DemoModule2Part>((context, demoModule) =>
            {
                int i = 0;
                ++i;
            });

            OnInitializing<DemoModule2Part>((context, demoModule) =>
            {
                int i = 0;
                ++i;
            });

            OnLoading<DemoModule2Part>((context, demoModule) =>
            {
                int i = 0;
                ++i;
            });

            OnLoaded<DemoModule2Part>((context, demoModule) =>
            {
                int i = 0;
                ++i;
            });
        }
    }
}