using Orchard.ContentManagement.Records;

namespace Contrib.RecentContent.Models {
    public class RecentContentWidgetPartRecord : ContentPartRecord {
        public virtual string ForContentPart { get; set; }
        public virtual int Count { get; set; }
        public virtual string OrderBy { get; set; }
    }
}
