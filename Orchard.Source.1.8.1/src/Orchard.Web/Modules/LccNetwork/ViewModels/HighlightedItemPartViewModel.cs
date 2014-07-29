using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LccNetwork.ViewModels
{
    public class HighlightedItemPartViewModel
    {
        public HighlightedItemPartViewModel()
        {
            HighlightGroups = new List<SelectListItem>();
        }

        public string HighlightGroup { get; set; }

        public List<SelectListItem> HighlightGroups { get; set; }
    }
}