using Orchard.UI.Resources;

namespace Bootstrap_3_1_1_SimpleBlog
{
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {

            var manifest = builder.Add();

            manifest.DefineScript("html5shiv").SetCdn("//oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js");
            manifest.DefineScript("respondJS").SetCdn("//oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js");
            manifest.DefineScript("Bootstrap").SetCdn("//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js").SetDependencies("jQuery");
            manifest.DefineScript("IE-10").SetUrl("IE-10.js").SetDependencies("jQuery");
            manifest.DefineScript("APA-Style").SetUrl("APA-Style.js").SetDependencies("jQuery");
                        
            manifest.DefineStyle("Site").SetUrl("Site.css");
        }
    }
}
