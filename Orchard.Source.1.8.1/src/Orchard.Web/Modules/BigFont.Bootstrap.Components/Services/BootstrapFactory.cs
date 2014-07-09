using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.DisplayManagement.Implementation;
using Orchard.DisplayManagement.Shapes;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Orchard.UI.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BigFont.Bootstrap.Components.Services
{
    [OrchardFeature("BigFont.Bootstrap.Components")]
    public class BootstrapFactory : IShapeFactoryEvents, IShapeDisplayEvents
    {
        private bool _processing;
        private bool _done = false;

        public BootstrapFactory() { }

        public void Creating(ShapeCreatingContext context) { }

        public void Created(ShapeCreatedContext context)
        {
            WrapFirstShape(context);
        }
        public void Displaying(ShapeDisplayingContext context) { }
        public void Displayed(ShapeDisplayedContext context) { }

        /// <summary>
        /// Wrap the first shape we come across so the wrapper can add CSS and JavaScript to the document.
        /// </summary>
        /// <remarks>
        /// The shape that we wrap tends to be the Page ContentItem
        /// </remarks>
        public void WrapFirstShape(ShapeCreatedContext context)
        {
            if (_done)
            {
                return;
            }

            // prevent reentrance as some methods could create new shapes, and trigger this event
            if (_processing)
            {
                return;
            }

            _processing = true;

            // wrap the first shape we come across, just so we can add JavaScript to the html document head
            context.Shape.Metadata.Wrappers.Add("BootstrapWrapper");
            _done = true;

            _processing = false;
        }
    }
}