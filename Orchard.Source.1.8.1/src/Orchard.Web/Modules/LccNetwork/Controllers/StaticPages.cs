using Orchard.Environment.Extensions;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LccNetwork.Controllers
{
    [Themed]
    [OrchardFeature("StaticPages")]
    public class StaticPages : Controller
    {
        public ViewResult FindAnLcc()
        {
            return View();
        }
    }
}