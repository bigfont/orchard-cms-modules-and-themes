using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap_3_1_1.Thumbnails.Models
{
    public class ThumbnailsPart : ContentPart<ThumbnailsRecord>
    {
        public string MediaFolder { get; set; }
    }
}