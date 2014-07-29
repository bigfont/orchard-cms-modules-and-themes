using LccNetwork.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Settings;
using LccNetwork.ViewModels;
using System.Web.Mvc;

namespace LccNetwork.Drivers
{
    public class HighlightedItemPartDriver : ContentPartDriver<HighlightedItemPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public HighlightedItemPartDriver(IContentManager contentManager, IContentDefinitionManager contentDefinitionManager)
        {
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
        }

        protected override DriverResult Display(HighlightedItemPart part, string displayType, dynamic shapeHelper)
        {
            var contentItem = GetHighlightedContentItem(null);

            return ContentShape("Parts_HighlightedItemPart", () => shapeHelper.Parts_HighlightedItemPart(item: contentItem));
        }

        protected override DriverResult Editor(HighlightedItemPart part, dynamic shapeHelper)
        {
            HighlightedItemPartViewModel viewModel = new HighlightedItemPartViewModel()
            {
                HighlightGroup = part.HighlightGroup,
                HighlightGroups = GetHighlightableTypeNames().Select(name => new SelectListItem() { Text = name, Value = name }).ToList()
            };

            return ContentShape("Parts_HighlightedItemPart_Edit", 
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/HighlightedItemPart",
                    Model: viewModel, 
                    Prefix: Prefix ));
        }

        private List<ContentItem> GetHighlightableContentItems()
        {
            string[] createableTypeNames = GetCreateableTypeNames();

            var highlightableContentItems = _contentManager
                .Query(createableTypeNames)
                .List()
                .ToList();

            return highlightableContentItems;
        }

        private ContentItem GetHighlightedContentItem(string highlightGroup)
        {            
            string[] createableTypeNames = GetCreateableTypeNames();

            var highlightedContentItem = _contentManager
                .Query(createableTypeNames)
                .Join<HighlightableItemPartRecord>()
                .Where(h => h.IsHighlighted)
                .List()
                .FirstOrDefault();

            return highlightedContentItem;
        }

        private string[] GetHighlightableTypeNames()
        {
            return _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(ctd => ctd.Parts.Any(cpd => cpd.PartDefinition.Name.Equals("HighlightableItemPart")))
                .Select(ctd => ctd.Name).ToArray<string>();
        }

        private string[] GetCreateableTypeNames()
        {
            return _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(ctd => ctd.Settings.GetModel<ContentTypeSettings>().Creatable)
                .Select(ctd => ctd.Name).ToArray<string>();
        }
    }
}