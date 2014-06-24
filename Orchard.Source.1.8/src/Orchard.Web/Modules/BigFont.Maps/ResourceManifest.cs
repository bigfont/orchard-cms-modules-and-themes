using Orchard.UI.Resources;

namespace Orchard.Blogs {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("BigFontMaps").SetUrl("BigFontMaps.css");
            manifest.DefineScript("GoogleMapsApi").SetUrl("http://maps.googleapis.com/maps/api/js?sensor=false");
            manifest.DefineScript("BigFontMap_MultipleMarkers").SetUrl("BigFontMap_MultipleMarkers.js").SetDependencies("GoogleMapsApi");
            manifest.DefineScript("BigFontMap_SingleMarker").SetUrl("BigFontMap_SingleMarker.js").SetDependencies("GoogleMapsApi");
        }
    }
}
