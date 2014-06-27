using Orchard.DisplayManagement.Implementation;
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
        public void Creating(ShapeCreatingContext context)
        {
            int i = 0;
            ++i;
        }
        public void Created(ShapeCreatedContext context)
        {
            int i = 0;
            ++i;
        }
        public void Displaying(ShapeDisplayingContext context)
        {
            int i = 0;
            ++i;
        }
        public void Displayed(ShapeDisplayedContext context)
        {
            int i = 0;
            ++i;
        }
    }
}