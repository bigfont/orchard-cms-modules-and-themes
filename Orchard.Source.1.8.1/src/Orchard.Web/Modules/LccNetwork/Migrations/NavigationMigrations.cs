using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace LccNetwork.Migrations
{
    [OrchardFeature("fea")]
    public class NavigationMigrations : DataMigrationImpl
    {
        public int Create()
        {
            return 1;
        }

        public int UpdateFrom1()
        {
            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterTypeDefinition("SectionMenu",
                    cfg => cfg
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
                    .WithPart("WidgetPart")
                    .WithPart("MenuWidgetPart") // why cannot we add MenuWidgetPart through the UI?
                    .WithSetting("Stereotype", "Widget")
                );

            return 3;
        }
    }
}