using Orchard.DisplayManagement.Implementation;
using Orchard.DisplayManagement.Shapes;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigFont.TheThemeMachineDesigner
{
    [OrchardFeature("BigFont.TheThemeMachineDesigner")]
    public class DesignerFactory : IShapeFactoryEvents, IShapeDisplayEvents
    {
        public void Creating(ShapeCreatingContext context) { }
        public void Created(ShapeCreatedContext context)
        {
            System.Diagnostics.Debug.Print(context.ShapeType);

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