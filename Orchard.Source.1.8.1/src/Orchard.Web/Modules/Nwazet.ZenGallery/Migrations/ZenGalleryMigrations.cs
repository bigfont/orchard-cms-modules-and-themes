using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Nwazet.ZenGallery.Migrations {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryMigrations : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable(
                "ZenGalleryPartRecord",
                table => table
                             .ContentPartRecord()
                             .Column<string>("Path", c => c.WithLength(2048)));

            ContentDefinitionManager.AlterPartDefinition("ZenGalleryPart", part => part.Attachable());

            ContentDefinitionManager.AlterTypeDefinition("ZenGallery",
                builder => builder
                    .WithPart("ZenGallery")
                    .WithPart("CommonPart")
                    .WithPart("TitlePart")
                    .WithPart("AutoroutePart", ctx => ctx
                        .WithSetting("AutorouteSettings.AllowCustomPattern", "true")
                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "false")
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{Name:'Title', Pattern: '{Content.Slug}', Description: 'my-gallery'}]")
                        .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))
                    .WithPart("ZenGalleryPart", ctx => ctx
                        .WithSetting("ZenGalleryPartSettings.PathPattern", "gallery/{Content.Slug}"))
                    .DisplayedAs("Gallery")
                    .Creatable()
                    .Draftable()
            );

            return 1;
        }
    }
}