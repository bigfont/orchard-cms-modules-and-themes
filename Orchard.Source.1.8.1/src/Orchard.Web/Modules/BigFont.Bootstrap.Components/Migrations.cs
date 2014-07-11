using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using System.Globalization;

namespace BigFont.Bootstrap.Components
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            /* 
             * "You can't add a field to a content type, 
             * you always add it to a content part. 
             * It's just that if that part has the same name as the type, 
             * nice magical things happen." - Bertrand Le Roy, Codeplex
             */

            ContentDefinitionManager.AlterPartDefinition("BootstrapThumbnails",
                builder => builder.WithField("ThumbnailImages",
                    fieldBuilder => fieldBuilder
                        .OfType("MediaLibraryPickerField")
                        .WithDisplayName("Thumbnail Images")
                        .WithSetting("MediaLibraryPickerFieldSettings.Hint", string.Empty)
                        .WithSetting("MediaLibraryPickerFieldSettings.Required", string.Empty)
                        .WithSetting("MediaLibraryPickerFieldSettings.Multiple", true.ToString(CultureInfo.InvariantCulture))
                        .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", string.Empty)
                        ));

            ContentDefinitionManager.AlterTypeDefinition("BootstrapThumbnails",
                builder => builder
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("BootstrapThumbnails",
                builder => builder
                    .WithPart("BootstrapThumbnails"));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterPartDefinition("BootstrapThumbnails",
                builder => builder.WithField("IntroImage",
                    fieldBuilder => fieldBuilder
                        .OfType("MediaLibraryPickerField")
                        .WithDisplayName("Intro Image")
                        .WithSetting("MediaLibraryPickerFieldSettings.Hint", string.Empty)
                        .WithSetting("MediaLibraryPickerFieldSettings.Required", string.Empty)
                        .WithSetting("MediaLibraryPickerFieldSettings.Multiple", false.ToString(CultureInfo.InvariantCulture))
                        .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", string.Empty)
            ));

            ContentDefinitionManager.AlterTypeDefinition("BootstrapThumbnails",
                builder => builder
                    .WithPart("BodyPart"));

            return 3;
        }

        public int UpdateFrom3()
        {
            // Removed below and then added again
            ContentDefinitionManager.AlterTypeDefinition("SubnavMenuItem",
                cfg => cfg
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .DisplayedAs("Subnav Menu Item")
                    .WithSetting("Description", "Todo")
                    .WithSetting("Stereotype", "MenuItem")
                );            

            return 4;
        }

        public int UpdateFrom4()
        {
            return 5;
        }

        public int UpdateFrom5()
        {
            return 6;
        }

        public int UpdateFrom6()
        {
            return 7;
        }

        public int UpdateFrom7()
        {
            return 8;
        }

        public int UpdateFrom8()
        {
            // Again, deprecated
            ContentDefinitionManager.AlterPartDefinition("WidgetPart",
                builder => builder.WithField("DisplayInSubnav", fieldBuilder => fieldBuilder
            .OfType("BooleanField")
            .WithDisplayName("Display in Subnav")
            .WithSetting("DisplayInSubnavSettings.Hint", "This is the hint.")));
            return 9;
        }

        public int UpdateFrom9()
        {
            // Deprecated and replaced with field
            // When do I add the field to the widget, though?
            ContentDefinitionManager.AlterPartDefinition("WidgetPart",
                builder => builder.WithField("DisplayInSubnav", fieldBuilder => fieldBuilder
                    .OfType("BooleanField")
                    .WithDisplayName("Display in Subnav")));
            return 10;
        }

        public int UpdateFrom10()
        {
            // Did I remove this, really? Aha. It's replace with the DisplayInSubnav field.
            ContentDefinitionManager.AlterPartDefinition("WidgetPart",
                builder => builder.RemoveField("DisplayInSubnav"));
            return 11;
        }

        public int UpdateFrom11()
        {
            ContentDefinitionManager.DeleteTypeDefinition("SubnavMenuItem");

            return 12;
        }

        public int UpdateFrom12()
        {
            // bootstrap subnav is deprecated
            ContentDefinitionManager.AlterTypeDefinition("BootstrapSubnav",
                builder => builder
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget")
                );

            return 13;
        }

        public int UpdateFrom13()
        {
            ContentDefinitionManager.AlterTypeDefinition("SubnavMenuItem",
                cfg => cfg
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .DisplayedAs("Subnav Menu Item")
                    .WithSetting("Description", "Todo")
                    .WithSetting("Stereotype", "MenuItem")
                );
            return 14;
        }

        public int UpdateFrom14()
        {
            ContentDefinitionManager.AlterTypeDefinition("SubnavMenuWidget", cfg => cfg
                .WithPart("CommonPart")
                .WithPart("IdentityPart")
                .WithPart("WidgetPart")
                .WithPart("MenuWidgetPart")
                .WithSetting("Stereotype", "Widget")
            );

            return 15;
        }

    }
}