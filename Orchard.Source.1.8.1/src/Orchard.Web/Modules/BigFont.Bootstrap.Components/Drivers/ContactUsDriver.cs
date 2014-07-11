using BigFont.Bootstrap.Components.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;

namespace BigFont.Bootstrap.Components.Drivers
{
    public class ContactUsDriver : ContentPartDriver<ContactUsPart>
    {
        protected override DriverResult Display(
            ContactUsPart part, string displayType, dynamic shapeHelper)
        {

            return ContentShape("Parts_ContactUs", () => shapeHelper.Parts_ContactUs(
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
            ContactUsPart part, dynamic shapeHelper)
        {

            return ContentShape("Parts_ContactUs_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/ContactUs",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            ContactUsPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}