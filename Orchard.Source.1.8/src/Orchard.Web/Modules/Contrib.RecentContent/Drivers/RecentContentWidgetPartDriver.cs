using System;
using System.Collections.Generic;
using System.Linq;
using Contrib.RecentContent.Models;
using Contrib.RecentContent.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;

namespace Contrib.RecentContent.Drivers {
    public class RecentContentWidgetPartDriver : ContentPartDriver<RecentContentWidgetPart> {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly Lazy<IEnumerable<IContentPartDriver>> _contentPartDrivers;

        public RecentContentWidgetPartDriver(
            IContentManager contentManager, 
            IContentDefinitionManager contentDefinitionManager,
            Lazy<IEnumerable<IContentPartDriver>> contentPartDrivers) {
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
            _contentPartDrivers = contentPartDrivers;
        }

        protected override DriverResult Display(RecentContentWidgetPart part, string displayType, dynamic shapeHelper) {
            // retrieve all content types implementing a specific content part
            var contentTypes = _contentManager.GetContentTypeDefinitions()
                .Where(x => x.Parts.Any(p => p.PartDefinition.Name == part.ForContentPart))
                .Select(x => x.Name)
                .ToArray();

            var query =_contentManager.Query(VersionOptions.Published, contentTypes)
                .Join<CommonPartRecord>();

            switch (part.OrderBy) {
                case "Created" : query = query.OrderByDescending(x => x.CreatedUtc);
                    break;
                case "Published" : query = query.OrderByDescending(x => x.PublishedUtc);
                    break;
                case "Modified" : query = query.OrderByDescending(x => x.ModifiedUtc);
                    break;
            }

            // build the Summary display for each content item
            var list = shapeHelper.List();
            list.AddRange(query.Slice(0, part.Count).Select(bp => _contentManager.BuildDisplay(bp, "Summary")));

            return ContentShape(shapeHelper.Parts_RecentContentWidget_List(ContentPart: part, ContentItems: list));
        }

        protected override DriverResult Editor(RecentContentWidgetPart part, dynamic shapeHelper) {
            var viewModel = new RecentContentWidgetViewModel {
                Part = part, 
                ContentPartNames = GetParts()
            };

            return ContentShape("Parts_RecentContent_Widget_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "Parts.RecentContent.Widget", Model: viewModel, Prefix: Prefix));
        }

        protected override DriverResult Editor(RecentContentWidgetPart part, IUpdateModel updater, dynamic shapeHelper) {
            var viewModel = new RecentContentWidgetViewModel {
                Part = part
            };

            updater.TryUpdateModel(viewModel, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(RecentContentWidgetPart part, ImportContentContext context) {
            part.ForContentPart = context.Attribute(part.PartDefinition.Name, "ForContentPart");
            part.Count = Int32.Parse(context.Attribute(part.PartDefinition.Name, "Count"));
            part.OrderBy = context.Attribute(part.PartDefinition.Name, "OrderBy");
        }

        protected override void Exporting(RecentContentWidgetPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("ForContentPart", part.ForContentPart);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Count", part.Count);
            context.Element(part.PartDefinition.Name).SetAttributeValue("OrderBy", part.OrderBy);
        }

        private IEnumerable<string> GetParts() {
            // user-defined parts
            var userContentParts = _contentDefinitionManager
                .ListPartDefinitions()
                .Select(x => x.Name);

            // code-defined parts
            var codeDefinedParts = _contentPartDrivers.Value.SelectMany(d => d.GetPartInfo().Where(cpd => !userContentParts.Any(m => m == cpd.PartName))).Select(x => x.PartName);

            return userContentParts.Union(codeDefinedParts).OrderBy(x => x);
        }
    }
}