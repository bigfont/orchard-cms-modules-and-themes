using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap_3_1_1.Thumbnails.Models
{
    public class ThumbnailsRecord : ContentPartRecord
    {
        public virtual string MediaFolder { get; set; }

    }
}