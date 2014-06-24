using System;
using System.Collections.Generic;
using System.Data;
using BigFont.Maps.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace BigFont.Maps.DataMigrations
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table BigFontMapRecord
            SchemaBuilder.CreateTable("BigFontMap_MultipleMarkersRecord", table => table
                .ContentPartRecord()
                .Column("Latitude", DbType.Double)
                .Column("Longitude", DbType.Double)
            );

            SchemaBuilder.CreateTable("BigFontMap_SingleMarkerRecord", table => table
                .ContentPartRecord()
                .Column("Latitude", DbType.Double)
                .Column("Longitude", DbType.Double)
            );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(BigFontMap_MultipleMarkersPart).Name, cfg => cfg.Attachable());


            ContentDefinitionManager.AlterPartDefinition(
                typeof(BigFontMap_SingleMarkerPart).Name, cfg => cfg.Attachable());

            return 1;
        }

        public int UpdateFrom1()
        {
            // Create a new widget content type with our map
            ContentDefinitionManager.AlterTypeDefinition("BigFontMap_MultipleMarkersWidget", cfg => cfg
                .WithPart("BigFontMap_MultipleMarkersPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 2;
        }

        public int UpdateFrom2()
        {
            // Create a new widget content type with our map
            ContentDefinitionManager.AlterTypeDefinition("BigFontMap_SingleMarkerWidget", cfg => cfg
                .WithPart("BigFontMap_SingleMarkerPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 3;
        }

        public int UpdateFrom3()
        {
            SchemaBuilder.AlterTable("BigFontMap_SingleMarkerRecord", table =>
            {
                table.AddColumn("TextDirections", DbType.String);
            });

            return 4;
        }
    }
}