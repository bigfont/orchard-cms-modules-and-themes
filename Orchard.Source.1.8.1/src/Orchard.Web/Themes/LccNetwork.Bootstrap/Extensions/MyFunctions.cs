using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System;
using Orchard.ContentManagement;
using Orchard.Utility.Extensions;

namespace LccNetwork.Bootstrap.Extensions
{
    public class MyFunctions
    {
        public static dynamic GetMainPartFromContentItem(ContentItem item)
        {
            // get the ContentPart that has the same name as the item's ContentType
            // so that we can access the item fields.
            var contentType = item.TypeDefinition.Name;
            var parts = item.Parts as List<ContentPart>;
            return parts.First(p => p.PartDefinition.Name.Equals(contentType));
        }

        public static dynamic GetMediaPartFromMediaLibraryPickerField(dynamic field, int index = 0)
        {
            // get the mediaPart at index in the media library picker field
            return field != null &&
                field.MediaParts != null &&
                field.MediaParts.Count > index ?
                field.MediaParts[index] : null;
        }

        public static dynamic GetTermPartFromTaxonomyField(dynamic field, int index = 0)
        {
            // get the termPart at index in the taxonomy field
            return field != null &&
                field.Terms != null &&
                field.Terms.Count > index ?
                field.Terms[index] : null;
        }
    }
}