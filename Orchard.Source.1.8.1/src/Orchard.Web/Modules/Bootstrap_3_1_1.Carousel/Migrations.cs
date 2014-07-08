using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Bootstrap_3_1_1.Carousel.Models;

namespace Bootstrap_3_1_1.Carousel
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table CarouselRecord
            SchemaBuilder.CreateTable("CarouselRecord", table => table
                .ContentPartRecord()
            );

            // Part definition
            ContentDefinitionManager.AlterPartDefinition(typeof(CarouselPart).Name, cfg 
                => cfg.Attachable());

            // Type definition
            ContentDefinitionManager.AlterTypeDefinition("CarouselWidget", cfg => cfg
                .WithPart("CarouselPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }
    }
}