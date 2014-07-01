using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc.Filters;
using Orchard.Environment.Features;
using Orchard.Environment.Extensions.Models;
using Orchard;
using Orchard.DisplayManagement;

namespace BigFont.TheThemeMachineDesigner.Filters
{
    public class SelectThemeFilter : FilterProvider, IResultFilter
    {
        private readonly IFeatureManager _featureManager;
        private readonly IWorkContextAccessor _workContextAccessor; 
        private readonly dynamic _shapeFactory;


        public SelectThemeFilter(IFeatureManager featureManager, IWorkContextAccessor workContextAccessor, IShapeFactory shapeFactory)
        {
            _featureManager = featureManager;
            _workContextAccessor = workContextAccessor;
            _shapeFactory = shapeFactory;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var installedThemes = _featureManager.GetEnabledFeatures()
                .Select(x => x.Extension)
                .Where(x => DefaultExtensionTypes.IsTheme(x.ExtensionType))
                .Distinct();

            var themeListItems = installedThemes
                .Select(theme => new SelectListItem
                {
                    Text = theme.Name,
                    Value = theme.Id
                })
                .ToList();

            _workContextAccessor.GetContext(filterContext).Layout.Zones["Body"].Add(_shapeFactory.ThemeSelect(Themes: themeListItems), ":before");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}