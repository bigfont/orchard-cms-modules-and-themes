using System;
using Orchard;
using Orchard.Localization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.Drivers;
using JetBrains.Annotations;
using BigFont.Bootstrap.Components.Models;

namespace BigFont.Bootstrap.Components
{
    [UsedImplicitly]
    public class DisplayInSubnavFieldDriver : ContentFieldDriver<DisplayInSubnavField>
    {
        public DisplayInSubnavFieldDriver(IOrchardServices services) {}

        private static string GetPrefix(ContentField field, ContentPart part)
        {
            // handles spaces in field names
            return (part.PartDefinition.Name + "." + field.Name)
                   .Replace(" ", "_");
        }

        protected override DriverResult Editor(
            ContentPart part, 
            DisplayInSubnavField field, 
            dynamic shapeHelper)
        {

            field.Value = field.Value ?? true; // set default

            return ContentShape("Fields_DisplayInSubnav_Edit",
                () => shapeHelper.EditorTemplate(
                          TemplateName: "Fields/DisplayInSubnav.Edit",
                          Model: field,
                          Prefix: GetPrefix(field, part)));
        }

        protected override DriverResult Editor(ContentPart part, DisplayInSubnavField field, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(field, GetPrefix(field, part), null, null);

            return ContentShape("Fields_DisplayInSubnav_Edit",
                () => shapeHelper.EditorTemplate(
                          TemplateName: "Fields/DisplayInSubnav.Edit",
                          Model: field,
                          Prefix: GetPrefix(field, part)));
        }
    }
}