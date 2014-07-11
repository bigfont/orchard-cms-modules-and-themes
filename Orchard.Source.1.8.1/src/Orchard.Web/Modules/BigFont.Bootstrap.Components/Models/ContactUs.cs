using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace BigFont.Bootstrap.Components.Models
{
    [OrchardFeature("Bootstrap.ContactUs")]
    public class ContactUsRecord : ContentPartRecord
    {
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string StreetAddress { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual string Country { get; set; }
        public virtual string PostalCode { get; set; }
    }
    [OrchardFeature("Bootstrap.ContactUs")]
    public class ContactUsPart : ContentPart<ContactUsRecord>
    {
        [Required]
        public string EmailAddress { get { return Record.EmailAddress; } set { Record.EmailAddress = value; } }
        [Required]
        public string PhoneNumber { get { return Record.PhoneNumber; } set { Record.PhoneNumber = value; } }
        [Required]
        public string StreetAddress { get { return Record.StreetAddress; } set { Record.StreetAddress = value; } }
        [Required]
        public string City { get { return Record.City; } set { Record.City = value; } }
        [Required]
        public string Province { get { return Record.Province; } set { Record.Province = value; } }
        [Required]
        public string Country { get { return Record.Country; } set { Record.Country = value; } }
        [Required]
        public string PostalCode { get { return Record.PostalCode; } set { Record.PostalCode = value; } }
    }
}