namespace BigFont.DemoModule
{
    using System.Data;
    using Orchard.ContentManagement.MetaData;
    using Orchard.Core.Contents.Extensions;
    using Orchard.Data.Migration;
    using BigFont.DemoModule.Models;
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            // Creating table DemoModuleRecord
            SchemaBuilder.CreateTable("DemoModuleRecord", table => table
                .ContentPartRecord()
                .Column("SomePropName", DbType.String)
            );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(DemoModulePart).Name, cfg => cfg.Attachable());

            return 1;
        }
    }
}