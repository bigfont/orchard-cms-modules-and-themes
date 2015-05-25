using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap_3_1_1_SimpleBlog
{
    using Orchard.DisplayManagement.Descriptors;
    public class ListShapeProvider : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            string alternateName = "List__Bootstrap__ListGroup";
            AddAlternateShape_OnFrontEnd(builder, alternateName, "List");
            AddAlternateShape_OnFrontEnd(builder, alternateName, "Parts_Blogs_BlogPost_List");
            AddAlternateShape_OnFrontEnd(builder, alternateName, "Parts_Blogs_RecentBlogPosts");
        }

        private void AddAlternateShape_OnFrontEnd(ShapeTableBuilder builder, string alternateName, string shapeName)
        {
            builder.Describe(shapeName).OnDisplaying(displaying =>
            {
                // make sure we are on the front end
                if (displaying.ShapeMetadata.DisplayType == null || !displaying.ShapeMetadata.DisplayType.Contains("Admin"))
                {
                    displaying.ShapeMetadata.Alternates.Add(alternateName);                
                }
            });
        }
    }
}