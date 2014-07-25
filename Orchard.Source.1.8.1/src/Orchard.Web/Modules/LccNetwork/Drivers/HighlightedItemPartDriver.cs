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

            return ContentShape("Parts_HighlightedItemPart", () => shapeHelper.Parts_HighlightedItemPart(
                item: contentItem
                ));
        }

        private ContentItem GetHighlightedContentItem(string highlightArea)
        {
            // get createable types
            string[] contentTypeNames = _contentDefinitionManager
                .ListTypeDefinitions()
                .Where(ctd => ctd.Settings.GetModel<ContentTypeSettings>().Creatable)
                .Select(ctd => ctd.Name).ToArray<string>();

            var contentItem = _contentManager
                .Query(contentTypeNames)                
                .List()
                .FirstOrDefault();

            return contentItem;
        }
    }
}