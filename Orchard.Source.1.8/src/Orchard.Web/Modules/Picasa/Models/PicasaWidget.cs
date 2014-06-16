using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel;

namespace Picasa.Models
{
    public class PicasaWidgetRecord : ContentPartRecord
    {
        public virtual string Username { get; set; }
    }

    public class PicasaWidgetPart : ContentPart<PicasaWidgetRecord>
    {
        [Required]
        [DisplayName("Picasa Username")]
        public string Username
        {
            get { return Record.Username; }
            set { Record.Username = value; }
        }

    }
}