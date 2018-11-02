using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class ResearchPassportModels
    {
        [Key]
        public int Id { get; set; }
        public  DateTime?  StartDate { get; set; }
        public  DateTime?  EndDate { get; set; }
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactsModels ContactName { get; set; }
        public  DateTime?  RenewalDate { get; set; }
    }
}