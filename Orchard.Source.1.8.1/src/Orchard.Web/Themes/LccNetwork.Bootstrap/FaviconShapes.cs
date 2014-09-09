using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;

namespace Vandelay.Industries.Shapes
{
    public class FaviconShapes : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;
        private string faviconUrl = "/Themes/LccNetwork.Bootstrap/Content/favicon.ico";

        public FaviconShapes(IWorkContextAccessor wca)
        {
            _wca = wca;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadLinks")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    // Get the current favicon from head
                    var resourceManager = _wca.GetContext().Resolve<IResourceManager>();
                    var links = resourceManager.GetRegisteredLinks();
                    var currentFavicon = links
                        .Where(l => l.Rel == "shortcut icon" && l.Type == "image/x-icon")
                        .FirstOrDefault();
                    // Modify if found
                    if (currentFavicon != default(LinkEntry))
                    {
                        currentFavicon.Href = faviconUrl;
                    }
                    else
                    {
                        // Add the new one
                        resourceManager.RegisterLink(new LinkEntry
                        {
                            Type = "image/x-icon",
                            Rel = "shortcut icon",
                            Href = faviconUrl
                        });
                    }
                });
        }
    }
}