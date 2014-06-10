namespace BigFont.DemoModule2
{
    using System.Data;
    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Contents.Extensions;
    using Orchard.Data.Migration;
    using BigFont.DemoModule2.Models;
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            // Creating table DemoModule2Record
            SchemaBuilder.CreateTable("DemoModule2Record", table => table
                .ContentPartRecord()
                .Column("SomePropName", DbType.String)
            );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(DemoModule2Part).Name, cfg => cfg.Attachable());

            return 1;
        }
    }
}