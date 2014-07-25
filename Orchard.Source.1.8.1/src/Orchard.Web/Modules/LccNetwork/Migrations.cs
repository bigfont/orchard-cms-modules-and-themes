using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using LccNetwork.Models;

namespace LccNetwork {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            SchemaBuilder.CreateTable("HighlightedItemPartRecord", table => table.ContentPartRecord());

            ContentDefinitionManager.AlterPartDefinition("HighlightedItemPart", builder => builder
                .WithDescription("Displays content items that the end user has chosen to highlight."));

            ContentDefinitionManager.AlterTypeDefinition("HighlightedItemWidget", builder => builder
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithPart("HighlightedItemPart")                
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }
    }
}