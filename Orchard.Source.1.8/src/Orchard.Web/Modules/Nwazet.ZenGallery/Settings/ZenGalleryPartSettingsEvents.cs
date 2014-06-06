﻿using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Environment.Extensions;

namespace Nwazet.ZenGallery.Settings {
    [OrchardFeature("Nwazet.ZenGallery")]
    public class ZenGalleryPartSettingsEvents : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "ZenGalleryPart")
                yield break;

            var settings = definition.Settings.GetModel<ZenGalleryPartSettings>();

            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "ZenGalleryPart")
                yield break;

            var settings = new ZenGalleryPartSettings {
                PathPattern = ""
            };

            if (updateModel.TryUpdateModel(settings, "ZenGalleryPartSettings", null, null)) {
                settings.Build(builder);
            }

            yield return DefinitionTemplate(settings);
        }
    }
}