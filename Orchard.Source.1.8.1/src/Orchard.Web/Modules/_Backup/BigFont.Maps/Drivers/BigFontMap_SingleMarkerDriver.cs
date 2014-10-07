using BigFont.Maps.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace BigFont.Maps.Drivers
{
    public class BigFontMap_SingleMarkerDriver : ContentPartDriver<BigFontMap_SingleMarkerPart>
    {
        protected override DriverResult Display(
            BigFontMap_SingleMarkerPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_BigFontMap_SingleMarker", () => shapeHelper.Parts_BigFontMap_SingleMarker(
                Longitude: part.Longitude, 
                Latitude: part.Latitude, 
                TextDirections: part.TextDirections
                ));
        }

        //GET
        protected override DriverResult Editor(BigFontMap_SingleMarkerPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_BigFontMap_SingleMarker_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "Parts/BigFontMap_SingleMarker", Model: part, Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(BigFontMap_SingleMarkerPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
