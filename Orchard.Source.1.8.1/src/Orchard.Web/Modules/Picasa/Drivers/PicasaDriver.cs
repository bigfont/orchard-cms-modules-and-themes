using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Picasa.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Picasa.Drivers
{
    public class PicasaDriver : ContentPartDriver<PicasaWidgetPart>
    {
        protected override DriverResult Display(PicasaWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Picasa", () => shapeHelper.Parts_Picasa(
                Username: part.Username
                ));
        }

        // GET
        protected override DriverResult Editor(PicasaWidgetPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Picasa_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/Picasa",
                    Model: part,
                    Prefix: Prefix));
        }

        //POST
        protected override DriverResult Editor(
            PicasaWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}