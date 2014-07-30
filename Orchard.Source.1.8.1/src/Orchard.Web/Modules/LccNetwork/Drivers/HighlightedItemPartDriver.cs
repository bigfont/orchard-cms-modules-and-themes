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
using Orchard.Environment.Extensions;

namespace LccNetwork.Drivers
{
    [OrchardFeature("Highlightable")]
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
            var contentItem = GetHighlightedContentItem(part.HighlightGroup);
            var display = _contentManager.BuildDisplay(contentItem, "Highlight");
            
            return ContentShape("Parts_HighlightedItemPart", () => shapeHelper.Parts_HighlightedItemPart(item: display));

        }

        protected override DriverResult Editor(HighlightedItemPart part, dynamic shapeHelper)
        {                        
            HighlightedItemPartViewModel viewModel = new HighlightedItemPartViewModel()
            {
                HighlightGroup = part.HighlightGroup,
                HighlightGroups = GetHighlightableTypeNamesAsSelectList()                    
            };

            return ContentShape("Parts_HighlightedItemPart_Edit", 
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/HighlightedItemPart",
                    Model: viewModel, 
                    Prefix: Prefix ));
        }        

        protected override DriverResult Editor(HighlightedItemPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        private List<SelectListItem> GetHighlightableTypeNamesAsSelectList()
        {
            return GetHighlightableTypeNames()
                .Select(name => new SelectListItem() { Text = name, Value = name })
                .ToList();
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

        private ContentItem GetHighlightedContentItem(string targetHighlightGroup)
        {            
            string[] createableTypeNames = GetCreateableTypeNames();

            var highlightedContentItem = _contentManager
                .Query(createableTypeNames)
                .Join<HighlightableItemPartRecord>()
                .Where(h => h.IsHighlighted && h.HighlightGroup.Equals(targetHighlightGroup)) // todo Use a int compare not a string compare for performance
                .List()
                .FirstOrDefault();

            return highlightedContentItem;
        }

        private List<string> GetHighlightableTypeNames()
        {
            var highlightableTypeNames = _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(ctd => ctd.Parts.Any(cpd => cpd.PartDefinition.Name.Equals("HighlightableItemPart")))
                .Select(ctd => ctd.Name)
                .ToList<string>();

            highlightableTypeNames.Add("All");

            return highlightableTypeNames;
        }

        private string[] GetCreateableTypeNames()
        {
            return _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(ctd => ctd.Settings.GetModel<ContentTypeSettings>().Creatable)
                .Select(ctd => ctd.Name)
                .ToArray<string>();
        }
    }
}