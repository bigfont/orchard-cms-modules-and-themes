using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Environment.Extensions;

namespace Nwazet.ZenGallery.Settings {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPartSettings {
        public string PathPattern { get; set; }

        public void Build(ContentTypePartDefinitionBuilder builder) {
            builder.WithSetting(
                "ZenGalleryPartSettings.PathPattern", PathPattern);
        }
    }
}