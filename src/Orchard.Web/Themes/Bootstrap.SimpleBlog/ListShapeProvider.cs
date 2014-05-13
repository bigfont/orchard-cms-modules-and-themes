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
                displaying.ShapeMetadata.Alternates.Add(
                    "List__Bootstrap__ListGroup");
            });

            builder.Describe("Parts_Blogs_BlogPost_List").OnDisplaying(displaying =>
            {
                displaying.ShapeMetadata.Alternates.Add(
                    "List__Bootstrap__ListGroup");
            });
        }
    }
}