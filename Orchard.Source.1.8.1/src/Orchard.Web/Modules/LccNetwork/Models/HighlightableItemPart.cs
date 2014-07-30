using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LccNetwork.Models
{    
    public class HighlightableItemPart : ContentPart<HighlightableItemPartRecord>
    {
        public bool IsHighlighted
        {
            get { return Retrieve(x => x.IsHighlighted); }
            set { Store(x => x.IsHighlighted, value); }
        }

        public string HighlightGroup
        {
            get { return Retrieve(x => x.HighlightGroup); }
            set { Store(x => x.HighlightGroup, value); }
        }
    }

    public class HighlightableItemPartRecord : ContentPartRecord
    {
        public virtual bool IsHighlighted { get; set; }
        public virtual string HighlightGroup { get; set; }
    }
}
