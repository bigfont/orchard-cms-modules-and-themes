using System;
using Orchard.ContentManagement;
using Orchard.ContentManagement.FieldStorage;
using Orchard.ContentManagement.MetaData.Models;

namespace BigFont.Bootstrap.Components.Models
{
    public class DisplayInSubnavField : ContentField
    {
        public bool? Value
        {
            get { return Storage.Get<bool?>(); }
            set { Storage.Set(value); }
        }
    }
}