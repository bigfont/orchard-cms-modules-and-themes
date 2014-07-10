using System.Web.Routing;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Title.Models;
using Orchard.Data;
using Orchard.Widgets.Models;
using System.Collections.Generic;
using System.Linq;
using Orchard.Fields.Fields;

namespace BigFont.Bootstrap.Components.Handlers
{
    [UsedImplicitly]
    public class WidgetPartHandler : ContentHandler
    {
        public WidgetPartHandler(IRepository<WidgetPartRecord> widgetsRepository) {
            OnInitializing<WidgetPart>((context, part) => (part as dynamic).ShowInSubnav.Value = true);
            OnLoading<WidgetPart>((context, part) => (part as dynamic).ShowInSubnav.Value = true);
        }
    }
}