using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using BigFont.ContactUs.Models;

namespace BigFont.ContactUs
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table BigFontContactUsRecord
            SchemaBuilder.CreateTable("BigFontContactUsRecord", table => table
                .ContentPartRecord()
                .Column("Latitude", DbType.Double)
                .Column("Longitude", DbType.Double)
            );

            ContentDefinitionManager.AlterPartDefinition(
    typeof(BigFontContactUsPart).Name, cfg => cfg.Attachable());

            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable("BigFontContactUsRecord", table =>
            {
                table.DropColumn("Latitude");
                table.DropColumn("Longitude");
                table.AddColumn("EmailAddress", DbType.String);
            });
                
            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.AlterTable("BigFontContactUsRecord", table =>
            {
                table.AddColumn("PhoneNumber", DbType.String);
                table.AddColumn("StreetAddress", DbType.String);
                table.AddColumn("City", DbType.String);
                table.AddColumn("Province", DbType.String);
                table.AddColumn("Country", DbType.String);
                table.AddColumn("PostalCode", DbType.String);
            });

            return 3;
        }


        public int UpdateFrom3()
        {
            // Create a new widget content type with our BigFontContactUs
            ContentDefinitionManager.AlterTypeDefinition("BigFontContactUsWidget", cfg => cfg
                .WithPart("BigFontContactUsPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 4;
        }
    }
}