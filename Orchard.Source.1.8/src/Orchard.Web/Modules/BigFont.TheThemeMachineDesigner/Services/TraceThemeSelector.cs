using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BigFont.TheThemeMachineDesigner.Services
{
    public class TraceThemeSelector : IThemeSelector
    {
        private readonly ITraceTheme _traceTheme;
        private const int ThemePriority = 90; // same as the PreviewTheme priority

        public TraceThemeSelector(ITraceTheme traceTheme)
        {
            _traceTheme = traceTheme;
        }

        public ThemeSelectorResult GetTheme(RequestContext context)
        {
            var traceThemeName = _traceTheme.GetTraceTheme();

            return string.IsNullOrEmpty(traceThemeName) ?
                null :
                new ThemeSelectorResult
                {
                    Priority = ThemePriority,
                    ThemeName = traceThemeName
                };
        }
    }
}