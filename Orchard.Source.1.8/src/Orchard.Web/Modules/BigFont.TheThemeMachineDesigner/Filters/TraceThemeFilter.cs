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
using BigFont.TheThemeMachineDesigner.Controllers;
using Orchard.Packaging.Models;
using Orchard.Packaging.Services;
using Orchard.Packaging.GalleryServer;
using System.Linq.Expressions;
using System.Collections;

namespace BigFont.TheThemeMachineDesigner.Filters
{
    public class TraceThemeFilter : FilterProvider, IResultFilter
    {
        private readonly IFeatureManager _featureManager;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly dynamic _shapeFactory;
        private readonly ISiteThemeService _siteThemeService;
        private readonly ITraceTheme _traceTheme;
        private readonly IPackagingSourceManager _packagingSourceManager;
        private readonly IOrchardServices _services;

        public TraceThemeFilter(
            IFeatureManager featureManager,
            IWorkContextAccessor workContextAccessor,
            IShapeFactory shapeFactory,
            ISiteThemeService siteThemeService,
            ITraceTheme traceTheme,
            IPackagingSourceManager packagingSourceManager,
            IOrchardServices services)
        {
            _featureManager = featureManager;
            _workContextAccessor = workContextAccessor;
            _shapeFactory = shapeFactory;
            _siteThemeService = siteThemeService;
            _traceTheme = traceTheme;
            _packagingSourceManager = packagingSourceManager;
            _services = services;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var currentTheme = GetCurrentThemeEntry();
            var tracedThemeId = _traceTheme.GetTraceThemeId();

            tracedThemeId = string.IsNullOrEmpty(tracedThemeId) ? currentTheme.Descriptor.Id : tracedThemeId;

            // installed themes            
            var installedThemes = this.GetInstalledThemes();
            var installedThemesList = this.ConvertExtensionDescriptorsToSelectList(installedThemes, tracedThemeId);

            // themes from gallery
            var allThemes = this.GetThemesFromGallery();
            var allThemesList = this.ConvertPackagingEntriesToSelectList(allThemes, tracedThemeId);

            // combine
            allThemesList.AddRange(installedThemesList);

            // render
            _workContextAccessor
                .GetContext(filterContext)
                .Layout
                .Zones["Body"]
                .Add(_shapeFactory.TraceThemeControls(Themes: allThemesList), ":before");
        }



        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        private List<SelectListItem> ConvertExtensionDescriptorsToSelectList(IEnumerable<ExtensionDescriptor> installedThemes, string tracedThemeId)
        {
            var installedThemesList = installedThemes.Select(theme => new SelectListItem
            {
                Text = theme.Name,
                Value = theme.Id,
                Selected = theme.Id == tracedThemeId
            })
                .ToList();

            return installedThemesList;
        }

        private IEnumerable<ExtensionDescriptor> GetInstalledThemes()
        {
            var installedThemes = _featureManager.GetEnabledFeatures()
                .Select(x => x.Extension)
                .Where(x => DefaultExtensionTypes.IsTheme(x.ExtensionType))
                .Distinct();

            return installedThemes;
        }

        private List<SelectListItem> ConvertPackagingEntriesToSelectList(IEnumerable<PackagingEntry> allThemes, string tracedThemeId)
        {
            var allThemesList = allThemes.Select(theme => new SelectListItem
            {
                Text = theme.Title,
                Value = theme.PackageId.Replace("Orchard.Theme.", string.Empty),
                Selected = theme.PackageId.Replace("Orchard.Theme.", string.Empty).Equals(tracedThemeId)
            })
            .ToList();

            return allThemesList;
        }

        private IEnumerable<PackagingEntry> GetThemesFromGallery()
        {
            var packageType = "Theme";
            var sources = _packagingSourceManager.GetSources();

            IEnumerable<PackagingEntry> extensions = null;
            foreach (var source in sources)
            {
                try
                {
                    Expression<Func<PublishedPackage, bool>> packagesCriteria = p =>
                        p.PackageType == packageType &&
                        p.IsLatestVersion;

                    var sourceExtensions = _packagingSourceManager.GetExtensionList(true,
                        source,
                        packages =>
                        {
                            packages = packages.Where(packagesCriteria);
                            packages = packages.OrderByDescending(p => p.DownloadCount).ThenBy(p => p.Title);
                            return packages;
                        }).ToArray();

                    extensions = extensions == null ? sourceExtensions : extensions.Concat(sourceExtensions);

                    // apply another paging rule in case there were multiple sources
                    if (sources.Count() > 1)
                    {
                        extensions = extensions.OrderByDescending(p => p.DownloadCount).ThenBy(p => p.Title);
                    }
                }
                catch (Exception)
                {
                    // todo handle exception
                }
            }

            extensions = extensions ?? new PackagingEntry[0];
            return extensions;
        }

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