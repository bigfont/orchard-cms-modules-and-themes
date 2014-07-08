using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Picasa.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Picasa.Handlers
{
    public class PicasaHandler : ContentHandler
    {
        public PicasaHandler(IRepository<PicasaWidgetRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}