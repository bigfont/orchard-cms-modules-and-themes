using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Bootstrap_3_1_1.Thumbnails.Models;
using Orchard.Data;

namespace Bootstrap_3_1_1.Thumbnails.Handlers
{
    public class ThumbnailsHandler : ContentHandler
    {
        public ThumbnailsHandler(IRepository<ThumbnailsRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}