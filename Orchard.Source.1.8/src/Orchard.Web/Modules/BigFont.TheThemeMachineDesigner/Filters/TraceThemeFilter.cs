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
using BigFont.TheThemeMachineDesigner.Services;

namespace BigFont.TheThemeMachineDesigner.Filters
{
    public class TraceThemeFilter : FilterProvider, IResultFilter
    {
        private readonly IFeatureManager _featureManager;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly dynamic _shapeFactory;
        private readonly ISiteThemeService _siteThemeService;
        private readonly ITraceTheme _traceTheme;
        private readonly IOrchardServices _services;

        public TraceThemeFilter(
            IFeatureManager featureManager,
            IWorkContextAccessor workContextAccessor,
            IShapeFactory shapeFactory,
            ISiteThemeService siteThemeService,
            ITraceTheme traceTheme,
            IOrchardServices services)
        {
            _featureManager = featureManager;
            _workContextAccessor = workContextAccessor;
            _shapeFactory = shapeFactory;
            _siteThemeService = siteThemeService;
            _traceTheme = traceTheme;
            _services = services;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var currentTheme = GetCurrentThemeEntry();
            var tracedThemeId = _traceTheme.GetTraceTheme();

            tracedThemeId = string.IsNullOrEmpty(tracedThemeId) ? currentTheme.Descriptor.Id : tracedThemeId;

            var installedThemes = _featureManager.GetEnabledFeatures()
                .Select(x => x.Extension)
                .Where(x => DefaultExtensionTypes.IsTheme(x.ExtensionType))
                .Distinct();

            var themeListItems = installedThemes.Select(theme => new SelectListItem
                {
                    Text = theme.Name,
                    Value = theme.Id,
                    Selected = theme.Id == tracedThemeId
                })
                .ToList();

            _workContextAccessor
                .GetContext(filterContext)
                .Layout
                .Zones["Body"]
                .Add(_shapeFactory.TraceThemeControls(Themes: themeListItems), ":before");
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