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

namespace BigFont.TheThemeMachineDesigner
{
    [OrchardFeature("BigFont.TheThemeMachineDesigner")]
    public class DesignerFactory : IShapeFactoryEvents, IShapeDisplayEvents
    {
        private readonly WorkContext _workContext;
        private readonly IAuthorizer _authorizer;
        private readonly IShapeTableManager _shapeTableManager;
        private bool _processing;
        private bool _done = false;

        private bool IsActivable()
        {
            // activate on front-end only
            if (AdminFilter.IsApplied(new RequestContext(_workContext.HttpContext, new RouteData())))
                return false;

            // if not logged as a site owner, still activate if it's a local request (development machine)
            if (!_authorizer.Authorize(StandardPermissions.SiteOwner))
                return _workContext.HttpContext.Request.IsLocal;

            return true;
        }

        public DesignerFactory(IWorkContextAccessor workContextAccessor, IAuthorizer authorizer, IShapeTableManager shapeTableManager)
        {
            _workContext = workContextAccessor.GetContext();
            _authorizer = authorizer;
            _shapeTableManager = shapeTableManager;
        }

        public void Creating(ShapeCreatingContext context) { }

        public void Created(ShapeCreatedContext context)
        {
            WrapFirstShape(context);
        }
        public void Displaying(ShapeDisplayingContext context) { }
        public void Displayed(ShapeDisplayedContext context) { }

        /// <summary>
        /// Wrap the first shape we come across so the wrapper can add JavaScript to the document head.
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

            if (!IsActivable())
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
            context.Shape.Metadata.Wrappers.Add("ThemeMachineDesignerWrapper");
            _done = true;

            _processing = false;
        }
    }
}