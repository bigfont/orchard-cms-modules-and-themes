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

namespace BigFont.TheThemeMachineDesigner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly IThemeService _themeService;
        private readonly IExtensionManager _extensionManager;
        private readonly ISiteThemeService _siteThemeService;
        private readonly Localizer T = NullLocalizer.Instance;
        private readonly ShellSettings _shellSettings;

        public HomeController(
            IOrchardServices services,
            IExtensionManager extensionManager,
            IThemeService themeService,
            ISiteThemeService siteThemeService,
            ShellSettings shellSettings)
        {
            _services = services;
            _themeService = themeService;
            _extensionManager = extensionManager;
            _siteThemeService = siteThemeService;
            _shellSettings = shellSettings;
        }

        [HttpPost]
        public ActionResult Activate(string themeId, string returnUrl)
        {
            //todo - add code contract

            _themeService.EnableThemeFeatures(themeId);
            _siteThemeService.SetSiteTheme(themeId);
            return this.RedirectLocal(returnUrl);
        }
    }
}