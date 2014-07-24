using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BigFont.Bootstrap.Components.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace BigFont.Bootstrap.Components.Handlers
{
    public class PicasaHandler : ContentHandler
    {
        public PicasaHandler(IRepository<PicasaRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}