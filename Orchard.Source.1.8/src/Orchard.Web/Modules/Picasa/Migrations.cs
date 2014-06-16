using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Picasa.Models;

namespace Picasa
{
    public class Migrations : DataMigrationImpl {

        public int Create() {
            // Creating table PicasaWidgetRecord
            SchemaBuilder.CreateTable("PicasaWidgetRecord", table => table
				.ContentPartRecord()
				.Column("Username", DbType.String)
			);

            ContentDefinitionManager.AlterPartDefinition(
                typeof(PicasaWidgetRecord).Name, cfg => cfg.Attachable());

			// Create a new widget content type with our map
            ContentDefinitionManager.AlterTypeDefinition("PicasaWidget", cfg => cfg
                .WithPart("PicasaWidgetPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));
				
            return 1;
        }
    }
}