using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using System.Globalization;
using BigFont.Bootstrap.Components.Models;

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

            return 1;
        }
     
        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("BootstrapThumbnails", builder => builder
                .WithField("ThumbnailImages", fieldBuilder => fieldBuilder
                    .OfType("MediaLibraryPickerField")
                    .WithDisplayName("Thumbnail Images")
                    .WithSetting("MediaLibraryPickerFieldSettings.Hint", string.Empty)
                    .WithSetting("MediaLibraryPickerFieldSettings.Required", string.Empty)
                    .WithSetting("MediaLibraryPickerFieldSettings.Multiple", true.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", string.Empty))
                .WithField("IntroImage", fieldBuilder => fieldBuilder
                    .OfType("MediaLibraryPickerField")
                    .WithDisplayName("Intro Image")
                    .WithSetting("MediaLibraryPickerFieldSettings.Hint", string.Empty)
                    .WithSetting("MediaLibraryPickerFieldSettings.Required", string.Empty)
                    .WithSetting("MediaLibraryPickerFieldSettings.Multiple", false.ToString(CultureInfo.InvariantCulture))
                    .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", string.Empty)
                    ));

            ContentDefinitionManager.AlterTypeDefinition("BootstrapThumbnails",
                builder => builder
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithPart("BodyPart")
                    .WithSetting("Stereotype", "Widget")
                    .WithPart("BootstrapThumbnails")
                );

            return 2;
        }

        // we previously ran the other updates during development
        // then consolidated them into one

        public int UpdateFrom22()
        {
            ContentDefinitionManager.AlterTypeDefinition("SubnavMenuItem",
                cfg => cfg
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .DisplayedAs("Subnav Menu Item")
                    .WithSetting("Description", "Display a list of internal links to headers within the page.")
                    .WithSetting("Stereotype", "MenuItem")
                );

            ContentDefinitionManager.AlterTypeDefinition("BootstrapSubnav", 
                cfg => cfg
                .WithPart("CommonPart")
                .WithPart("IdentityPart")
                .WithPart("WidgetPart")
                .WithPart("MenuWidgetPart")
                .WithSetting("Stereotype", "Widget")
            );

            ContentDefinitionManager.AlterPartDefinition("WidgetPart", cfg => cfg.WithField("DisplayInSubnav", 
                builder => builder.OfType("DisplayInSubnavField")));

            return 23;
        }

        public int UpdateFrom29()
        {
            // are these calls still necessary?
            ContentDefinitionManager.DeleteTypeDefinition("ContactUs");
            SchemaBuilder.DropTable("ContactUsRecord");
            // end

            SchemaBuilder.CreateTable("ContactUsRecord", table => table
                .ContentPartRecord()
                .Column("PhoneNumber", DbType.String)
                .Column("EmailAddress", DbType.String)
                .Column("StreetAddress", DbType.String)
                .Column("City", DbType.String)
                .Column("Province", DbType.String)
                .Column("Country", DbType.String)
                .Column("PostalCode", DbType.String)
            );

            ContentDefinitionManager.AlterPartDefinition("ContactUsPart", cfg => cfg.Attachable());

            ContentDefinitionManager.AlterTypeDefinition("BootstrapContactUs", cfg => cfg
                .WithPart("ContactUsPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            return 30;
        }
    }
}