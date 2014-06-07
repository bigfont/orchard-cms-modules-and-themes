using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Bootstrap_3_1_1.Thumbnails.Models;

namespace Bootstrap_3_1_1.Thumbnails
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {

            // Creating table CarouselRecord
            SchemaBuilder.CreateTable("ThumbnailsRecord", table => table
                .ContentPartRecord()
            );

            // Part definition
            ContentDefinitionManager.AlterPartDefinition(typeof(ThumbnailsPart).Name, cfg
                => cfg.Attachable());

            // Type definition
            ContentDefinitionManager.AlterTypeDefinition("ThumbnailsWidget", cfg => cfg
                .WithPart("ThumbnailsPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("ThumbnailsPart", cfg => cfg
                .RemovePart("WidgetPart")
                .WithSetting("Stereotype", "")
                .Creatable()
                .DisplayedAs("Thumbnails"));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("ThumbnailsPart", cfg => cfg
                .Draftable()
                .WithPart("TitlePart"));

            return 3;
        }

        public int UpdateFrom3()
        {
            // begin again
            ContentDefinitionManager.DeletePartDefinition("ThumbnailsPart");
            ContentDefinitionManager.DeleteTypeDefinition("ThumbnailsWidget");
            ContentDefinitionManager.DeleteTypeDefinition("ThumbnailsPart");

            ContentDefinitionManager.AlterPartDefinition(typeof(ThumbnailsPart).Name, cfg
                => cfg.Attachable());

            ContentDefinitionManager.AlterTypeDefinition("Thumbnails", cfg => cfg
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("ThumbnailsPart")
                .DisplayedAs("Thumbnails"));

            return 4;
        }

        public int UpdateFrom4()
        {
            SchemaBuilder.AlterTable("ThumbnailsRecord", table => table
                .AddColumn<string>("MediaFolder", c => c.WithLength(2048)));

            return 5;
        }
    }
}