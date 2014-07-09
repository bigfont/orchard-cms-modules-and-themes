using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using System.Globalization;

namespace Bootstrap_3_1_1
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
    }
}