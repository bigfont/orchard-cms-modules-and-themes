using System.Collections.Generic;
using Contrib.RecentContent.Models;

namespace Contrib.RecentContent.ViewModels {
    public class RecentContentWidgetViewModel {
        public RecentContentWidgetPart Part { get; set; }
        public IEnumerable<string> ContentPartNames { get; set; }
    }
}