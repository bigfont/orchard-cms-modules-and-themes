using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LccNetwork.Models
{
    // todo maybe Change the name of this to HighlightedItemWidget
    public class HighlightedItemPart : ContentPart<HighlightedItemPartRecord>
    {
        public string HighlightGroup
        {
            get { return Retrieve(x => x.HighlightGroup); }
            set { Store(x => x.HighlightGroup, value); }
        }
    }

    public class HighlightedItemPartRecord : ContentPartRecord
    {
        public virtual string HighlightGroup { get; set; }
    }
}