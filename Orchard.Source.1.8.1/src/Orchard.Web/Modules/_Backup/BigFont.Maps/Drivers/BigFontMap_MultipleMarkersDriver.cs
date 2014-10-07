using BigFont.Maps.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace BigFont.Maps.Drivers {
    public class BigFontMap_MultipleMarkersDriver : ContentPartDriver<BigFontMap_MultipleMarkersPart> {
        protected override DriverResult Display(BigFontMap_MultipleMarkersPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_BigFontMap_MultipleMarkers",
                                () => shapeHelper.Parts_BigFontMap_MultipleMarkers(Longitude: part.Longitude, Latitude: part.Latitude));
        }

        //GET
        protected override DriverResult Editor(BigFontMap_MultipleMarkersPart part, dynamic shapeHelper) {
            return ContentShape("Parts_BigFontMap_MultipleMarkers_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "Parts/BigFontMap_MultipleMarkers", Model: part, Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(BigFontMap_MultipleMarkersPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
