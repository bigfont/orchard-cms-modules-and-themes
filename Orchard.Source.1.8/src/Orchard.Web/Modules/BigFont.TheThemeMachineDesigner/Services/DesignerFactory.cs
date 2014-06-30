using Orchard;
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

        public DesignerFactory(IWorkContextAccessor workContextAccessor, IAuthorizer authorizer)
        {
            _workContext = workContextAccessor.GetContext();
            _authorizer = authorizer;
        }

        public void Creating(ShapeCreatingContext context) { }
        public void Created(ShapeCreatedContext context)
        {
            if (!IsActivable())
            {
                return;
            }            

            if (context.ShapeType != "Layout"
                && context.ShapeType != "DocumentZone"
                && context.ShapeType != "PlaceChildContent"
                && context.ShapeType != "ContentZone"
                && context.ShapeType != "ShapeTracingMeta"
                && context.ShapeType != "ShapeTracingTemplates"
                && context.ShapeType != "DateTimeRelative")
            {
                var shapeMetadata = (ShapeMetadata)context.Shape.Metadata;
                shapeMetadata.Wrappers.Add("ThemeMachineDesignerWrapper");
            }
        }
        public void Displaying(ShapeDisplayingContext context) { }
        public void Displayed(ShapeDisplayedContext context) { }
    }
}