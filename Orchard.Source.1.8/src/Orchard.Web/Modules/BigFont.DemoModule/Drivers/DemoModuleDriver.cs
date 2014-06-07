namespace BigFont.DemoModule.Drivers
{
    using BigFont.DemoModule.Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    public class DemoModuleDriver : ContentPartDriver<DemoModulePart>
    {
        protected override DriverResult Display(
            DemoModulePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_DemoModule", () => shapeHelper.Parts_DemoModule(
                MediaFolderName: part.SomePropName));
        }

        //GET
        protected override DriverResult Editor(
            DemoModulePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_DemoModule_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/DemoModule",
                    Model: part,
                    Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(
            DemoModulePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}