using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using LccNetwork.Models;
using System.Globalization;
using Orchard.Environment.Extensions;

namespace LccNetwork.Migrations
{
    [OrchardFeature("Highlightable")]
    public class HighlightableMigrations : DataMigrationImpl
    {
        private void Reset()
        {
            SchemaBuilder.DropTable(typeof(HighlightedItemPartRecord).Name);
            SchemaBuilder.DropTable(typeof(HighlightableItemPartRecord).Name);
            ContentDefinitionManager.DeleteTypeDefinition("HighlightedItemWidget");

            ContentDefinitionManager.DeletePartDefinition(typeof(HighlightedItemPart).Name);
            ContentDefinitionManager.DeletePartDefinition(typeof(HighlightableItemPart).Name);
            ContentDefinitionManager.DeletePartDefinition(typeof(HighlightableItemPart).Name);
        }

        public int Create()
        {
            Reset();

            SchemaBuilder.CreateTable(typeof(HighlightedItemPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<string>("HighlightGroup"));

            ContentDefinitionManager.AlterPartDefinition(typeof(HighlightedItemPart).Name, builder => builder
                .WithDescription("Displays content items that the end user has chosen to highlight."));

            ContentDefinitionManager.AlterTypeDefinition("HighlightedItemWidget", builder => builder
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithPart(typeof(HighlightedItemPart).Name)
                .WithSetting("Stereotype", "Widget"));

            SchemaBuilder.CreateTable(typeof(HighlightableItemPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<string>("HighlightGroup")
                .Column<bool>("IsHighlighted", c => c.WithDefault(false)));

            ContentDefinitionManager.AlterPartDefinition(typeof(HighlightableItemPart).Name, builder => builder
                .Attachable()
                .WithDescription("Lets a user add a content item to the Highlighted Item Widget."));            

            return 1;
        }
    }
}