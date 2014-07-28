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
using Orchard.Fields.Settings;

namespace LccNetwork
{
    public class Migrations : DataMigrationImpl
    {
        private Action<ContentPartFieldDefinitionBuilder> LearnMore;
        private Action<ContentPartFieldDefinitionBuilder> SingleImage;

        // the ctor is for code reuse
        public Migrations()
        {            
            LearnMore = new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("LinkField")
                .WithSetting("LinkFieldSettings.Hint", "A website address associated with this item.")
                .WithSetting("LinkFieldSettings.Required", false.ToString(CultureInfo.InvariantCulture))
                .WithSetting("LinkFieldSettings.TargetMode", TargetMode.None.ToString())
                .WithSetting("LinkFieldSettings.LinkTextMode", LinkTextMode.Optional.ToString())
                .WithSetting("LinkFieldSettings.StaticText", string.Empty));

            SingleImage = new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("MediaLibraryPickerField")
                .WithDisplayName("Image")
                .WithSetting("MediaLibraryPickerFieldSettings.Hint", "An image to display with this item.")
                .WithSetting("MediaLibraryPickerFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.Multiple", false.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", "Image"));
        }

        public int Create()
        {
            SchemaBuilder.DropTable(typeof(HighlightedItemPartRecord).Name);

            SchemaBuilder.CreateTable(typeof(HighlightedItemPartRecord).Name, table => table.ContentPartRecord());

            ContentDefinitionManager.AlterPartDefinition(typeof(HighlightedItemPart).Name, builder => builder
                .WithDescription("Displays content items that the end user has chosen to highlight."));

            ContentDefinitionManager.AlterTypeDefinition("HighlightedItemWidget", builder => builder
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithPart("HighlightedItemPart")
                .WithSetting("Stereotype", "Widget"));

            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.DropTable(typeof(HighlightableItemPartRecord).Name);

            SchemaBuilder.CreateTable(typeof(HighlightableItemPartRecord).Name, table => table
                .ContentPartRecord()
                .Column<bool>("IsHighlighted", c => c.WithDefault(false)));

            ContentDefinitionManager.AlterPartDefinition(typeof(HighlightableItemPart).Name, builder => builder
                .Attachable()
                .WithDescription("Lets a user add a content item to the Highlighted Item Widget."));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterPartDefinition("NewsItem", builder => builder
               .WithDescription("Contains fields for the News Item Content Type.")
               .WithField("LearnMore", LearnMore)
               .WithField("Image", SingleImage)
               .WithField("Summary", field => field
                   .OfType("TextField")
                   .WithDisplayName("Summary")
                   .WithSetting("TextFieldSettings.Flavor", "Text")
                   .WithSetting("TextFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("TextFieldSettings.Hint", "A summary of the news item."))
               .WithField("ShortTitle", field => field
                   .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "Short Title")
                   .WithSetting("InputFieldSettings.Hint", "A shorter version of the news item's title.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))               
               .WithField("Date", field => field
                   .OfType("DateTimeField")
                   .WithSetting("DateTimeFieldSettings.Display", DateTimeFieldDisplays.DateAndTime.ToString())
                   .WithSetting("DateTimeFieldSettings.Hint", "The publish date of the news item.")
                   .WithSetting("DateTimeFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture)))
               );

            ContentDefinitionManager.AlterTypeDefinition("NewsItem", builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithPart("NewsItem")
                .WithPart(typeof(HighlightableItemPart).Name));

            return 3;
        }

        public int UpdateFrom3()
        {
            ContentDefinitionManager.AlterPartDefinition("Event", builder => builder
               .WithDescription("Contains fields for the Event Content Type.")
               .WithField("LearnMore", LearnMore)
                .WithField("StartDate", field => field
                   .OfType("DateTimeField")                                
                   .WithSetting("DateTimeFieldSettings.Display", DateTimeFieldDisplays.DateAndTime.ToString())
                   .WithSetting("DateTimeFieldSettings.Hint", "The date the event starts.")
                   .WithSetting("DateTimeFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture)))
                .WithField("EndDate", field => field
                   .OfType("DateTimeField")                   
                   .WithSetting("DateTimeFieldSettings.Display", DateTimeFieldDisplays.DateAndTime.ToString())
                   .WithSetting("DateTimeFieldSettings.Hint", "The date the event ends.")
                   .WithSetting("DateTimeFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture)))
                // todo maybe - replace the following with a single LocationPart that has settings
                .WithField("City", field => field
                   .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "City")
                   .WithSetting("InputFieldSettings.Hint", "Add the event's city.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                .WithField("State", field => field
                   .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "State")
                   .WithSetting("InputFieldSettings.Hint", "Add the event's state.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                );

            ContentDefinitionManager.AlterTypeDefinition("Event", builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithPart("Event")
                .WithPart(typeof(HighlightableItemPart).Name));

            return 4;
        }

        public int UpdateFrom4()
        {
            ContentDefinitionManager.AlterPartDefinition("Spotlight", builder => builder
                .WithDescription("This is often an Lcc to highlight on the home page")
                .WithField("LearnMore", LearnMore)
                .WithField("Image", SingleImage));

            ContentDefinitionManager.AlterTypeDefinition("Spotlight", builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("Spotlight")
                .WithPart(typeof(HighlightableItemPart).Name));


            return 5;
        }
    }
}