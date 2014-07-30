using LccNetwork.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LccNetwork.Drivers
{
    [OrchardFeature("Highlightable")]
    public class HighlightableItemPartDriver : ContentPartDriver<HighlightableItemPart>
    {
        //GET
        protected override DriverResult Editor(HighlightableItemPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_HighlightableItemPart_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/HighlightableItemPart",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(HighlightableItemPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}