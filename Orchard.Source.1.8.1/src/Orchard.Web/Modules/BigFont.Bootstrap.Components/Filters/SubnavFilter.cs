using System.Collections.Generic;
using System.Linq;
using Orchard.Localization;
using Orchard.UI.Navigation;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;
using Orchard.Widgets;

namespace BigFont.Bootstrap.Components.Filters
{
    public class SubnavFilter : INavigationFilter
    {
        private readonly IWidgetsService _widgetService;

        public SubnavFilter(IWidgetsService widgetsService)
        {
            _widgetService = widgetsService;
        }

        public IEnumerable<MenuItem> Filter(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
            {
                if (item.Content != null && item.Content.ContentItem.ContentType == "SubnavMenuItem")
                {
                    var widgets = GetWidgetsThatAppearInSubnav();

                    var menuPosition = item.Position;
                    int index = 0;
                    foreach (var w in widgets)
                    {
                        var inserted = new MenuItem
                        {
                            Text = new LocalizedString(w.Title),
                            Url = string.Format("#{0}", w.Title.ToLowerInvariant()), // todo make this the html id
                            Items = new MenuItem[0],
                            Position = menuPosition + ":" + index++,
                        };

                        yield return inserted;
                    }
                }
                else
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<WidgetPart> GetWidgetsThatAppearInSubnav()
        {
            IEnumerable<WidgetPart> widgets = _widgetService
                .GetWidgets()
                .Where(w => (w as dynamic).DisplayInSubnav.Value == true);
            return widgets;
        }
    }
}