using Autofac.Features.Metadata;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigFont.FingerPrint
{
    [OrchardSuppressDependency("Orchard.UI.Resources.ResourceManager")]
    public class FingerPrintResourceManager : ResourceManager
    {
        public FingerPrintResourceManager(IEnumerable<Meta<IResourceManifestProvider>> resourceProviders)
            : base(resourceProviders)
        {
            System.Diagnostics.Debugger.Break();
        }
    }
}