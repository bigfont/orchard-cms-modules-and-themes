using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Orchard;
using Orchard.Utility.Extensions;
using Orchard.Themes;
using Orchard.Themes.Services;
using Orchard.Localization;
using Orchard.Environment.Extensions;
using Orchard.Environment.Extensions.Models;
using Orchard.Environment.Configuration;
using Orchard.Mvc;
using Orchard.Mvc.Extensions;
using BigFont.TheThemeMachineDesigner.Services;

namespace BigFont.TheThemeMachineDesigner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly IThemeService _themeService;
        private readonly IExtensionManager _extensionManager;
        private readonly ISiteThemeService _siteThemeService;
        private readonly Localizer T = NullLocalizer.Instance;
        private readonly ITraceTheme _traceTheme;
        private readonly ShellSettings _shellSettings;

        public HomeController(
            IOrchardServices services,
            IExtensionManager extensionManager,
            IThemeService themeService,
            ISiteThemeService siteThemeService,
            ITraceTheme traceTheme,
            ShellSettings shellSettings)
        {
            _services = services;
            _themeService = themeService;
            _extensionManager = extensionManager;
            _siteThemeService = siteThemeService;
            _traceTheme = traceTheme;
            _shellSettings = shellSettings;
        }

        [HttpPost]
        public ActionResult Apply(string themeId, string returnUrl)
        {
            //todo - add code contracts

            _themeService.EnableThemeFeatures(themeId);
            _siteThemeService.SetSiteTheme(themeId);
            return this.RedirectLocal(returnUrl);
        }

        [HttpPost]
        public ActionResult Trace(string themeId, string returnUrl)
        {
            //todo - add code contracts

            _themeService.EnableThemeFeatures(themeId);
            _traceTheme.SetTraceTheme(themeId);
            return this.RedirectLocal(returnUrl);
        }
    }
}