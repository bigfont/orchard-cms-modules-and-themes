using Orchard.UI.Resources;

namespace Bootstrap_3_1_1_Base {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("Bootstrap").SetUrl("Bootstrap/bootstrap.min.js").SetVersion("3.2.0");
            manifest.DefineStyle("Bootstrap").SetUrl("Bootstrap/less/bootstrap.min.css").SetVersion("3.2.0");
        }
    }
}
