#define RESET_THE_DATABASE

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
using System.Text.RegularExpressions;

namespace LccNetwork
{
    [OrchardFeature("LccNetwork")]
    public class LccNetworkMigrations : DataMigrationImpl
    {
        private Action<ContentPartFieldDefinitionBuilder>
            LearnMoreLinkField,
            SummaryTextAreaField,
            SingleImageField,
            MultipleDocumentsField,
            WorkAreasTaxonomyField,
            ResourcesTaxonomyField,
            LccTaxonomyField;

        private List<string> LccContentTypesNames = new List<string>();

        private void Reset()
        {
            LccContentTypesNames.ForEach(s =>
            {

                ContentDefinitionManager.DeletePartDefinition(s);
                ContentDefinitionManager.DeleteTypeDefinition(s);

            });
        }

        // ctor for code reuse
        public LccNetworkMigrations()
        {
            LearnMoreLinkField = BuildLinkField("Learn More", "An external link to learn more about this item.", false, TargetMode.NewWindow, LinkTextMode.Static, "Learn More");

            SummaryTextAreaField = BuildTextField("Summary", "Text", false, "A summary of this item.");

            SingleImageField = BuildMediaLibraryPickerField("Image", "An image to display with this item.", false, false, "Image");

            MultipleDocumentsField = BuildMediaLibraryPickerField("Documents", "Add documents associated with this item.", false, true, "Document");

            LccTaxonomyField = BuildTaxonomyField("Associated Lcc", true, "Lccs", false, false, false, string.Empty, string.Empty, false);

            ResourcesTaxonomyField = BuildTaxonomyField("Resource Type", true, "Resources", false, false, false, string.Empty, string.Empty, false);

            WorkAreasTaxonomyField = BuildTaxonomyField("Work Area", true, "Work Areas", false, false, false, string.Empty, string.Empty, false);
        }

        #region Field and Part Helpers
        private string Slugify(string s)
        {
            return Regex.Replace(s, "(?<!^)([A-Z])", "-$1").ToLower();
        }

        private Action<ContentTypePartDefinitionBuilder> BuildAutorouteSettings(bool allowCustomPattern, bool autoAdjustOnEdit,
            string patternName, string pattern, string patternDescription, int defaultPatternIndex)
        {
            string patternDefinition = string.Format(@"Name:'{0}', Pattern:'{1}', Description:'{2}'", patternName, pattern, patternDescription);
            return new Action<ContentTypePartDefinitionBuilder>(part => part
                        .WithSetting("AutorouteSettings.AllowCustomPattern", allowCustomPattern.ToString())
                        .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", autoAdjustOnEdit.ToString())
                        .WithSetting("AutorouteSettings.PatternDefinitions", "[{" + patternDefinition + "}]")
                        .WithSetting("AutorouteSettings.DefaultPatternIndex", defaultPatternIndex.ToString()));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildLinkField(string displayName, string hint, bool required, TargetMode targetMode, LinkTextMode linkTextMode, string staticText)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("LinkField")
                .WithDisplayName(displayName)
                .WithSetting("LinkFieldSettings.Hint", hint)
                .WithSetting("LinkFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture))
                .WithSetting("LinkFieldSettings.TargetMode", targetMode.ToString())
                .WithSetting("LinkFieldSettings.LinkTextMode", linkTextMode.ToString())
                .WithSetting("LinkFieldSettings.StaticText", staticText));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildTextField(string displayName, string flavor, bool required, string hint)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("TextField")
                .WithDisplayName(displayName)
                .WithSetting("TextFieldSettings.Flavor", flavor)
                .WithSetting("TextFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture))
                .WithSetting("TextFieldSettings.Hint", hint));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildMediaLibraryPickerField(string displayName, string hint, bool required, bool multiple, string displayedContentTypes)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("MediaLibraryPickerField")
                .WithDisplayName(displayName)
                .WithSetting("MediaLibraryPickerFieldSettings.Hint", hint)
                .WithSetting("MediaLibraryPickerFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.Multiple", multiple.ToString(CultureInfo.InvariantCulture))
                .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", displayedContentTypes));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildInputField(string displayName, string type, string title, string hint, bool required, bool autoFocus, bool autoComplete, string placeHolder, string editorCssClass, int maxLength, string pattern)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                .OfType("InputField")
                .WithDisplayName(displayName)
                .WithSetting("InputFieldSettings.Type", type)
                .WithSetting("InputFieldSettings.Title", title)
                .WithSetting("InputFieldSettings.Hint", hint)
                .WithSetting("InputFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture))
                .WithSetting("InputFieldSettings.AutoFocus", autoFocus.ToString(CultureInfo.InvariantCulture))
                .WithSetting("InputFieldSettings.AutoComplete", autoComplete.ToString(CultureInfo.InvariantCulture))
                .WithSetting("InputFieldSettings.PlaceHolder", placeHolder)
                .WithSetting("InputFieldSettings.EditorCssClass", editorCssClass)
                .WithSetting("InputFieldSettings.MaxLength", maxLength.ToString(CultureInfo.InvariantCulture))
                .WithSetting("InputFieldSettings.Pattern", pattern));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildDateTimeField(string displayName, DateTimeFieldDisplays display, string hint, bool required)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                    .OfType("DateTimeField")
                    .WithDisplayName(displayName)
                    .WithSetting("DateTimeFieldSettings.DateTimeFieldDisplays", display.ToString())
                    .WithSetting("DateTimeFieldSettings.Hint", hint)
                    .WithSetting("DateTimeFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture)));
        }

        private Action<ContentPartFieldDefinitionBuilder> BuildTaxonomyField(string displayName, bool allowCustomTerms, string taxonomy, bool leavesOnly, bool singleChoice, bool autoComplete, string hint, string taxonomies, bool required)
        {
            return new Action<ContentPartFieldDefinitionBuilder>(field => field
                    .OfType("TaxonomyField")
                    .WithDisplayName(displayName)
                    .WithSetting("TaxonomyFieldSettings.AllowCustomTerms", allowCustomTerms.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("TaxonomyFieldSettings.Taxonomy", taxonomy)
                    .WithSetting("TaxonomyFieldSettings.LeavesOnly", leavesOnly.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("TaxonomyFieldSettings.SingleChoice", singleChoice.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("TaxonomyFieldSettings.AutoComplete", autoComplete.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("TaxonomyFieldSettings.Hint", hint)
                    .WithSetting("TaxonomyFieldSettings.Taxonomies", taxonomies)
                    .WithSetting("TaxonomyFieldSettings.Required", required.ToString(CultureInfo.InvariantCulture)));
        }

        #endregion

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
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("Contains fields for the News Item Content Type.")
                .WithField("LearnMore", this.LearnMoreLinkField)
                .WithField("Image", this.SingleImageField)
                .WithField("Summary", this.SummaryTextAreaField)
                .WithField("ShortTitle", this.BuildInputField("Short Title", "Text", "Short Title", "A shorter version of the news item's title.", true, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("Date", this.BuildDateTimeField("Date", DateTimeFieldDisplays.DateAndTime, "The publish date of the news item.", false))
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                .WithPart(typeName));

            return 2;
        }

        public int UpdateFrom2()
        {
            var typeName = "Event";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
               .WithDescription("Contains fields for the Event Content Type.")
                .WithField("LearmMore", this.LearnMoreLinkField)
                .WithField("StartDate", this.BuildDateTimeField("Start Date", DateTimeFieldDisplays.DateAndTime, "The date the event starts.", false))
                .WithField("EndDate", this.BuildDateTimeField("End Date", DateTimeFieldDisplays.DateAndTime, "The date the event starts.", false))
                // todo maybe - replace the following with a single LocationPart that has settings
                .WithField("City", this.BuildInputField("City", "Text", "City", "Add the event's city.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("State", this.BuildInputField("State", "Text", "State", "Add the event's State.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                .WithPart(typeName));

            return 3;
        }

        public int UpdateFrom3()
        {
            var typeName = "Spotlight";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("This is often an Lcc to highlight on the home page")
                .WithField("LearmMore", this.LearnMoreLinkField)
                .WithField("Image", this.SingleImageField));

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("TitlePart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                .WithPart(typeName));

            return 4;
        }

        public int UpdateFrom4()
        {
            var typeName = "Staff";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("A staff member of an LCC")
                .WithField("FirstName", this.BuildInputField("First Name", "Text", "First Name", "Add the staff member's first name.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("LastName", this.BuildInputField("Last Name", "Text", "Last Name", "Add the staff member's last name.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("Position", this.BuildInputField("Position", "Text", "Position", "Add the staff member's position.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("Email", this.BuildInputField("Email", "Email", "Email", "Add the staff member's email.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("Lcc", this.LccTaxonomyField)
            );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                .Creatable()
                .Draftable()
                .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                .WithPart(typeName));

            return 5;
        }

        public int UpdateFrom5()
        {
            var typeName = "CouncilMeeting";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("An LCC meeting")
                .WithField("City", this.BuildInputField("City", "Text", "City", "Add the city.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("State", this.BuildInputField("State", "Text", "State", "Add the state.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("StartDate", this.BuildDateTimeField("Start Date", DateTimeFieldDisplays.DateAndTime, "Add the meeting start date.", false))
                .WithField("EndDate", this.BuildDateTimeField("End Date", DateTimeFieldDisplays.DateAndTime, "Add the meeting end date.", false))
                .WithField("Link", this.BuildLinkField("Link", "An internal address associated with this meeting.", false, TargetMode.None, LinkTextMode.Optional, string.Empty))
                .WithField("MeetingOutcome", this.BuildTextField("Meeting Outcome", "Html", false, "Give a sense of the meeting outcome."))
                .WithField("Documents", this.MultipleDocumentsField)
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                    .Creatable()
                    .Draftable()
                    .WithPart("TitlePart")
                    .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                    .WithPart(typeName));

            return 6;
        }

        public int UpdateFrom6()
        {
            var typeName = "WorkItem";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("A work item")
                .WithField("LearmMore", this.LearnMoreLinkField)
                .WithField("Image", this.SingleImageField)
                .WithField("Summary", this.SummaryTextAreaField)
                .WithField("Area", this.WorkAreasTaxonomyField)
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                    .Creatable()
                    .Draftable()
                    .WithPart("TitlePart")
                    .WithPart("BodyPart")
                    .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                    .WithPart(typeName));

            return 7;
        }

        public int UpdateFrom7()
        {
            var typeName = "Resource";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("A resource item")
                .WithField("Documents", this.MultipleDocumentsField)
                .WithField("Lcc", this.LccTaxonomyField)
                .WithField("Resource Type", this.ResourcesTaxonomyField)
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                    .Creatable()
                    .Draftable()
                    .WithPart("TitlePart")
                    .WithPart("BodyPart")
                    .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                    .WithPart(typeName));

            return 8;
        }

        public int UpdateFrom8()
        {
            var typeName = "FundingOpportunity";
            this.LccContentTypesNames.Add(typeName);

            ContentDefinitionManager.AlterPartDefinition(typeName, builder => builder
                .WithDescription("A funding opportunity item")
                .WithField("DueDate", this.BuildDateTimeField("Due Date", DateTimeFieldDisplays.DateAndTime, "The application deadline.", false))
                .WithField("DueDateCaption", this.BuildInputField("Due Date Caption", "Text", "Due Date Caption", "An explaination of the due date.", false, false, false, string.Empty, string.Empty, 150, string.Empty))
                .WithField("Link", this.BuildLinkField("Link", "A link to more information about the opportunity.", false, TargetMode.None, LinkTextMode.Optional, string.Empty))
                );

            ContentDefinitionManager.AlterTypeDefinition(typeName, builder => builder
                    .Creatable()
                    .Draftable()
                    .WithPart("TitlePart")
                    .WithPart("BodyPart")
                    .WithPart("CommonPart")
                    .WithPart("AutoroutePart", this.BuildAutorouteSettings(true, true, typeName, Slugify(typeName) + "/{Content.Slug}", "Type/Slug", 0))
                    .WithPart(typeName));

            return 9;
        }
    }

}