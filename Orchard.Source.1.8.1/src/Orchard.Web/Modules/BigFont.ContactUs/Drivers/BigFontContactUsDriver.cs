using BigFont.ContactUs.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

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

        protected override void Importing(BigFontContactUsPart part, ImportContentContext context)
        {
            var emailAddress = context.Attribute(part.PartDefinition.Name, "EmailAddress");
            if (emailAddress != null)
            {
                part.EmailAddress = emailAddress;
            }

            var phoneNumber = context.Attribute(part.PartDefinition.Name, "PhoneNumber");
            if (phoneNumber != null)
            {
                part.PhoneNumber = phoneNumber;
            }

            var streetAddress = context.Attribute(part.PartDefinition.Name, "StreetAddress");
            if (streetAddress != null)
            {
                part.StreetAddress = streetAddress;
            }

            var city = context.Attribute(part.PartDefinition.Name, "City");
            if (city != null)
            {
                part.City = city; 
            }

            var province = context.Attribute(part.PartDefinition.Name, "Province");
            if (province != null)
            {
                part.Province = province; 
            }

            var country = context.Attribute(part.PartDefinition.Name, "Country");
            if (country != null)
            {
                part.Country = country; 
            }

            var postalCode = context.Attribute(part.PartDefinition.Name, "PostalCode");
            if (postalCode != null)
            {
                part.PostalCode = postalCode; 
            }
        }
    }
}