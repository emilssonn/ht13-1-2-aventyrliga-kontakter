using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventurousContacts.Models
{
    [MetadataType(typeof(Contact_Metadata))]
    public partial class Contact
    {
        //If the contact is new = not created yet
        public bool IsNew 
        { 
            get
            {
                return this.ContactID == 0;
            }
        }

        public class Contact_Metadata
        {
            private const int maxLength = 50;

            [Required]
            [MaxLength(maxLength)]
            [EmailAddress]
            [Display(Name = "E-mail address")]
            public string EmailAddress { get; set; }

            [Required]
            [MaxLength(maxLength)]
            [Display(Name = "First name")]
            public string FirstName { get; set; }

            [Required]
            [MaxLength(maxLength)]
            [Display(Name = "Last name")]
            public string LastName { get; set; }
        }
    }
}