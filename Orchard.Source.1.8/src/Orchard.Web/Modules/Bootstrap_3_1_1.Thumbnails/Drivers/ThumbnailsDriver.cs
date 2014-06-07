using Bootstrap_3_1_1.Thumbnails.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap_3_1_1.Thumbnails.Drivers
{
    public class ThumbnailsDriver : ContentPartDriver<ThumbnailsPart>
    {
        protected override DriverResult Display(ThumbnailsPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Thumbnails", () => shapeHelper.Parts_Thumbnails());
        }

        protected override DriverResult Editor(ThumbnailsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater != null)
            {
                bool result = updater.TryUpdateModel(part, Prefix, null, null);
            }

            return Editor(part, shapeHelper);
            
            //return ContentShape("Parts_Thumbnails_Edit",
            //    () => shapeHelper.EditorTemplate(TemplateName: "Parts/Thumbnails", Model:part));
        }
    }
}