using Orchard.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigFont.TheThemeMachineDesigner.Services
{
    public class TraceTheme : ITraceTheme
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly string DesignerThemeKey = typeof(TraceTheme).FullName;

        public TraceTheme(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTraceThemeId()
        {
            var httpContext = _httpContextAccessor.Current();
            if (httpContext.Session != null)
            {
                return Convert.ToString(httpContext.Session[DesignerThemeKey]);
            }

            return null;
        }

        public void SetTraceTheme(string themeName)
        {
            var httpContext = _httpContextAccessor.Current();
            if (httpContext.Session != null)
            {
                if (string.IsNullOrEmpty(themeName))
                {
                    httpContext.Session.Remove(DesignerThemeKey);
                }
                else
                {
                    httpContext.Session[DesignerThemeKey] = themeName;
                }
            }
        }
    }
}