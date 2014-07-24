using Orchard.UI.Resources;

namespace BigFont.Bootstrap.Components
{
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("Bootstrap").SetUrl("Bootstrap/bootstrap.min.js").SetVersion("3.1.1").SetDependencies("jQuery");
            manifest.DefineStyle("Bootstrap").SetUrl("Bootstrap/bootstrap.min.css").SetVersion("3.1.1");
        }
    }
}
