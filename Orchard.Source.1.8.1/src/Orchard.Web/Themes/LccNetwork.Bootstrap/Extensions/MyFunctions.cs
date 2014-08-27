using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System;
using Orchard.ContentManagement;
using Orchard.Utility.Extensions;
using System.ComponentModel;
using System.Web.Mvc;
using Orchard.MediaLibrary.Models;
using System.Text;

// these aren't really extention methods
// because they don't take 'this' as the first argument
namespace LccNetwork.Bootstrap.Extensions
{
    public class MyFunctions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var obj = propertyDescriptor.GetValue(anonymousObject);
                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }

        public static void SortContentItemsByAssociatedLcc(List<ContentItem> contentItems)
        {
            // sort by associated lcc
            Comparison<ContentItem> comparison = (item1, item2) =>
            {
                // part
                dynamic mainPart1 = MyFunctions.GetMainPartFromContentItem(item1);
                dynamic mainPart2 = MyFunctions.GetMainPartFromContentItem(item2);
                // term
                var lccTermPart1 = MyFunctions.GetTermPartFromTaxonomyField(mainPart1.Lcc);
                var lccTermPart2 = MyFunctions.GetTermPartFromTaxonomyField(mainPart2.Lcc);
                // lcc
                var associatedLcc1 = lccTermPart1 != null ? lccTermPart1.Name : string.Empty;
                var associatedLcc2 = lccTermPart2 != null ? lccTermPart2.Name : string.Empty;

                return string.Compare(associatedLcc1, associatedLcc2);
            };

            contentItems.Sort(comparison);
        }

        public static dynamic GetMainPartFromContentItem(ContentItem item)
        {
            // Get the ContentPart that has the same name as the item's ContentType
            // so that we can access the item fields.
            // This method just avoids having to use the .ContentTypeName notation that I found annoying.
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