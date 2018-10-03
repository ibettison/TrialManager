using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class ContactsModels
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [DisplayName("Organisation")]
        public string Organisation { get; set; }

        [DisplayName("Telephone Number")]
        public string Telephone { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("Contact Status")]
        public int ContactStatusId { get; set; }

        [DisplayName("Contact Notes")]
        public string ContactNotes { get; set; }

        [ForeignKey("ContactStatusId")]
        public virtual ContactStatusModels ContactStatusName { get; set; }

    }
}