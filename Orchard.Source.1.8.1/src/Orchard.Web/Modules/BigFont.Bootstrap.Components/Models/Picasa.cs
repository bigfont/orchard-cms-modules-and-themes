using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel;

namespace BigFont.Bootstrap.Components.Models
{
    public class PicasaRecord : ContentPartRecord
    {
        public virtual string Username { get; set; }
    }

    public class PicasaPart : ContentPart<PicasaRecord>
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