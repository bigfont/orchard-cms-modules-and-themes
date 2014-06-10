namespace BigFont.DemoModule2.Drivers
{
    using BigFont.DemoModule2.Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    public class DemoModule2Driver : ContentPartDriver<DemoModule2Part>
    {
        protected override DriverResult Display(
            DemoModule2Part part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DemoModule2", () => shapeHelper.Parts_DemoModule2(
                MediaFolderName: part.SomePropName));
        }

        //GET
        protected override DriverResult Editor(
            DemoModule2Part part, dynamic shapeHelper)
        {
            return ContentShape("Parts_DemoModule2_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/DemoModule2",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            DemoModule2Part part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}