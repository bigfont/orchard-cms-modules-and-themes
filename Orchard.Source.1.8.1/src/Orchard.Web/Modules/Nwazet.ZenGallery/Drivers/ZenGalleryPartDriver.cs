using System;
using Nwazet.ZenGallery.Models;
using Nwazet.ZenGallery.Services;
using Nwazet.ZenGallery.Settings;
using Nwazet.ZenGallery.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Localization;

namespace Nwazet.ZenGallery.Drivers {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPartDriver : ContentPartDriver<ZenGalleryPart> {
        private readonly IZenGalleryService _zenGalleryService;

        public ZenGalleryPartDriver(
            IZenGalleryService zenGalleryService) {
            _zenGalleryService = zenGalleryService;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix { get { return "ZenGallery"; }}

        protected override DriverResult Display(ZenGalleryPart part, string displayType, dynamic shapeHelper) {
            var images = _zenGalleryService.GetImages(part);
            return ContentShape(
                "Parts_ZenGallery",
                () => shapeHelper.Parts_ZenGallery(
                    ContentPart: part,
                    Images: images,
                    Path: part.Path));
        }

        protected override DriverResult Editor(ZenGalleryPart part, dynamic shapeHelper) {
            return Editor(part, null, shapeHelper);
        }

        protected override DriverResult Editor(ZenGalleryPart part, IUpdateModel updater, dynamic shapeHelper) {

            var settings = part.TypePartDefinition.Settings.GetModel<ZenGalleryPartSettings>();
            
            var viewModel = new ZenGalleryPartEditViewModel {
                Path = part.Path,
                PathPattern = settings.PathPattern,
                Images = _zenGalleryService.GetImages(part)
            };

            if (updater != null && updater.TryUpdateModel(viewModel, Prefix, null, null)) {

                part.Path = string.IsNullOrWhiteSpace(viewModel.Path) ?
                    _zenGalleryService.GeneratePath(part) : viewModel.Path;
                
                // remove any leading slash in the path
                while (!string.IsNullOrEmpty(part.Path) && part.Path[0] == '/') {
                    part.Path = part.Path.Substring(1);
                }

                if (!_zenGalleryService.IsPathValid(part.Path)) {
                    if (part.Path[0] == '.' || part.Path.EndsWith("."))
                        updater.AddModelError("Path", T("The \".\" can't be used at either end of the path."));
                    else
                        updater.AddModelError("Path", T("Please do not use any of the following characters in your path: \":\", \"?\", \"#\", \"[\", \"]\", \"@\", \"!\", \"$\", \"&\", \"'\", \"(\", \")\", \"*\", \"+\", \",\", \";\", \"=\", \", \"<\", \">\", \"\\\". No spaces are allowed (please use dashes or underscores instead)."));
                }
            }

            return ContentShape("Parts_ZenGallery_Edit", 
                () => shapeHelper.EditorTemplate(TemplateName: "Parts/ZenGallery", Model: viewModel, Prefix: Prefix));
        }

        protected override void Importing(ZenGalleryPart part, ImportContentContext context) {
            var path = context.Attribute(part.PartDefinition.Name, "Path");
            if (path != null) {
                part.Path = path;
            }
        }

        protected override void Exporting(ZenGalleryPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Path", String.IsNullOrEmpty(part.Record.Path) ? "/" : part.Record.Path);
        }
    }
}
