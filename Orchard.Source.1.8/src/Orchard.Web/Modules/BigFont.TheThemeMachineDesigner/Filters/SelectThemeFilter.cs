using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Orchard;
using Orchard.Mvc.Filters;
using Orchard.Environment.Features;
using Orchard.Environment.Extensions.Models;
using Orchard.DisplayManagement;
using Orchard.Themes;
using Orchard.Themes.Services;
using Orchard.Themes.Models;

namespace BigFont.TheThemeMachineDesigner.Filters
{
    public class SelectThemeFilter : FilterProvider, IResultFilter
    {
        private readonly IFeatureManager _featureManager;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly dynamic _shapeFactory;
        private readonly ISiteThemeService _siteThemeService;
        private readonly IOrchardServices _services;

        public SelectThemeFilter(
            IFeatureManager featureManager,
            IWorkContextAccessor workContextAccessor,
            IShapeFactory shapeFactory,
            ISiteThemeService siteThemeService,
            IOrchardServices services)
        {
            _featureManager = featureManager;
            _workContextAccessor = workContextAccessor;
            _shapeFactory = shapeFactory;
            _siteThemeService = siteThemeService;
            _services = services;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // only let authorized users apply themes
            if (!_services.Authorizer.Authorize(Permissions.ApplyTheme))
            {
                return;
            }

            var currentTheme = GetCurrentThemeEntry();

            var installedThemes = _featureManager.GetEnabledFeatures()
                .Select(x => x.Extension)
                .Where(x => DefaultExtensionTypes.IsTheme(x.ExtensionType))
                .Distinct();

            var themeListItems = installedThemes
                .Select(theme => new SelectListItem
                {
                    Text = theme.Name,
                    Value = theme.Id,
                    Selected = theme.Name == currentTheme.Name
                })
                .ToList();

            _workContextAccessor.GetContext(filterContext).Layout.Zones["Body"].Add(_shapeFactory.ThemeSelect(Themes: themeListItems), ":before");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        private ThemeEntry GetCurrentThemeEntry()
        {
            ThemeEntry currentTheme = null;
            ExtensionDescriptor currentThemeDescriptor = _siteThemeService.GetSiteTheme();
            if (currentThemeDescriptor != null)
            {
                currentTheme = new ThemeEntry(currentThemeDescriptor);
            }
            return currentTheme;
        }
    }
}