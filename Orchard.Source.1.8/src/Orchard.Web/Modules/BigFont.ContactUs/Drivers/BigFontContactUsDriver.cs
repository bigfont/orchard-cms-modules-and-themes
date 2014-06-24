using BigFont.ContactUs.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace BigFont.ContactUs.Drivers
{
    public class BigFontContactUsDriver : ContentPartDriver<BigFontContactUsPart>
    {
        protected override DriverResult Display(
            BigFontContactUsPart part, string displayType, dynamic shapeHelper)
        {

            return ContentShape("Parts_BigFontContactUs", () => shapeHelper.Parts_BigFontContactUs(
                EmailAddress: part.EmailAddress,
                PhoneNumber: part.PhoneNumber,
                StreetAddress: part.StreetAddress,
                City: part.City,
                Province: part.Province,
                Country: part.Country,
                PostalCode: part.PostalCode
                ));
        }

        //GET
        protected override DriverResult Editor(
            BigFontContactUsPart part, dynamic shapeHelper)
        {

            return ContentShape("Parts_BigFontContactUs_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/BigFontContactUs",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            BigFontContactUsPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}