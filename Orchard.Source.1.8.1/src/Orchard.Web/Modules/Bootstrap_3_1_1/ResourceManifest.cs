using Orchard.UI.Resources;

namespace Bootstrap_3_1_1 {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("Bootstrap").SetUrl("Bootstrap/bootstrap.min.css").SetVersion("3.1.1");
        }
    }
}
