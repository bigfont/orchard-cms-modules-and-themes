using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace Contrib.RecentContent {
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("RecentContentWidgetPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("ForContentPart")
                    .Column<string>("OrderBy")
                    .Column<int>("Count")
                );

            ContentDefinitionManager.AlterTypeDefinition("RecentContent",
                cfg => cfg
                    .WithPart("RecentContentWidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            ContentDefinitionManager.AlterTypeDefinition("RecentContent",
                cfg => cfg
                    .WithPart("RecentContentWidgetPart")
                );
            return 1;
        }
    }
}