using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.SimpleBlog
{
    using Orchard.DisplayManagement.Descriptors;
    public class ListShapeProvider : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("List").OnDisplaying(displaying =>
            {
                // make sure we are on the front end
                if (displaying.ShapeMetadata.DisplayType == null || !displaying.ShapeMetadata.DisplayType.Contains("Admin"))
                {
                    displaying.ShapeMetadata.Alternates.Add("List__Bootstrap__ListGroup");
                }
            });

            builder.Describe("Parts_Blogs_BlogPost_List").OnDisplaying(displaying =>
            {
                // make sure we are on the front end
                if (displaying.ShapeMetadata.DisplayType == null || !displaying.ShapeMetadata.DisplayType.Contains("Admin"))
                {
                    displaying.ShapeMetadata.Alternates.Add("List__Bootstrap__ListGroup");
                }
            });
        }

        private void AddAlternateShapeOnFrontEnd(ShapeTableBuilder builder, string shapeName, string alternateName)
        {
            builder.Describe(shapeName).OnDisplaying(displaying =>
            {
                // make sure we are on the front end
                if (!displaying.ShapeMetadata.DisplayType.Contains("Admin"))
                {
                    displaying.ShapeMetadata.Alternates.Add(alternateName);                
                }
            });
        }
    }
}