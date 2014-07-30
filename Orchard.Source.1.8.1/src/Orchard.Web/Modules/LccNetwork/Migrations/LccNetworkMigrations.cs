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
using Orchard.Environment.Extensions;

namespace LccNetwork
{
    [OrchardFeature("LccNetwork")]
    public class LccNetworkMigrations : DataMigrationImpl
    {
        private Action<ContentPartFieldDefinitionBuilder> LearnMoreField;
        private Action<ContentPartFieldDefinitionBuilder> SingleImageField;

        private void Reset()
        {
            ContentDefinitionManager.DeletePartDefinition("NewsItem");
            ContentDefinitionManager.DeletePartDefinition("Event");
            ContentDefinitionManager.DeletePartDefinition("Spotlight");

            ContentDefinitionManager.DeleteTypeDefinition("NewsItem");
            ContentDefinitionManager.DeleteTypeDefinition("Event");
            ContentDefinitionManager.DeleteTypeDefinition("Spotlight");
        }

        // ctor for code reuse
        public LccNetworkMigrations()
        {
            LearnMoreField = new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("LinkField")
                .WithSetting("LinkFieldSettings.Hint", "A website address associated with this item.")
                .WithSetting("LinkFieldSettings.Required", false.ToString(CultureInfo.InvariantCulture))
                .WithSetting("LinkFieldSettings.TargetMode", TargetMode.None.ToString())
                .WithSetting("LinkFieldSettings.LinkTextMode", LinkTextMode.Optional.ToString())
                .WithSetting("LinkFieldSettings.StaticText", string.Empty));

            SingleImageField = new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("MediaLibraryPickerField")
                .WithDisplayName("Image")
                .WithSetting("MediaLibraryPickerFieldSettings.Hint", "An image to display with this item.")
                .WithSetting("MediaLibraryPickerFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.Multiple", false.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", "Image"));
        }

        public int Create()
        {
#if RESET_THE_DATABASE
            Reset();
#endif

            return 1;
        }

        public int UpdateFrom1()
        {
            var typeName = "NewsItem";

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
               .WithDescription("Contains fields for the News Item Content Type.")
               .WithField("LearnMoreField", LearnMoreField)
               .WithField("Image", SingleImageField)
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

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithPart("HighlightableItemPart")
                .WithPart(typeName));

            return 2;
        }

        public int UpdateFrom2()
        {
            var typeName = "Event";

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
               .WithDescription("Contains fields for the Event Content Type.")
               .WithField("LearnMoreField", LearnMoreField)
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

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithPart("HighlightableItemPart")
                .WithPart(typeName));

            return 3;
        }

        public int UpdateFrom3()
        {
            var typeName = "Spotlight";

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("This is often an Lcc to highlight on the home page")
                .WithField("LearnMoreField", LearnMoreField)
                .WithField("Image", SingleImageField));

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("HighlightableItemPart")
                .WithPart(typeName));

            return 4;
        }

        public int UpdateFrom4()
        {
            var typeName = "Staff";

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("A staff member of an LCC")
                .WithField("FirstName", field => field
                    .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "First Name")
                   .WithSetting("InputFieldSettings.Hint", "Add the staff member's first name.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                .WithField("LastName", field => field
                    .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "Last Name")
                   .WithSetting("InputFieldSettings.Hint", "Add the staff member's last name.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                .WithField("Position", field => field
                    .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Text")
                   .WithSetting("InputFieldSettings.Title", "Position")
                   .WithSetting("InputFieldSettings.Hint", "Add the staff member's position.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                .WithField("Email", field => field
                    .OfType("InputField")
                   .WithSetting("InputFieldSettings.Type", "Email")
                   .WithSetting("InputFieldSettings.Title", "Email")
                   .WithSetting("InputFieldSettings.Hint", "Add the staff member's email.")
                   .WithSetting("InputFieldSettings.Required", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoFocus", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("InputFieldSettings.PlaceHolder", string.Empty)
                   .WithSetting("InputFieldSettings.EditorCssClass", string.Empty)
                   .WithSetting("InputFieldSettings.MaxLength", "0")
                   .WithSetting("InputFieldSettings.Pattern", string.Empty))
                .WithField("Lcc", field => field
                    .OfType("TaxonomyField")
                   .WithSetting("TaxonomyFieldSettings.AllowCustomTerms", true.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("TaxonomyFieldSettings.Taxonomy", "Lcc")
                   .WithSetting("TaxonomyFieldSettings.LeavesOnly", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("TaxonomyFieldSettings.SingleChoice", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("TaxonomyFieldSettings.AutoComplete", false.ToString(CultureInfo.InvariantCulture))
                   .WithSetting("TaxonomyFieldSettings.Hint", string.Empty)
                   .WithSetting("TaxonomyFieldSettings.Taxonomies", string.Empty)
                   .WithSetting("TaxonomyFieldSettings.Required", false.ToString(CultureInfo.InvariantCulture)))
                   );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()             
                .WithPart(typeName));

            return 5;
        }
    }
}