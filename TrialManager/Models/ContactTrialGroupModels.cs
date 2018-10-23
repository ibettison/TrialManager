using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Models
{
    public class ContactTrialGroupModels
    {
        [Key]
        public int Id { get; set; }

        public int TrialGroupId { get; set; }
        public int ContactId { get; set; }
        public bool ReadOnly { get; set; }
        [ForeignKey("ContactId")]
        public virtual ContactsModels ContactName { get; set; }
        [ForeignKey("TrialGroupId")]
        public virtual TrialGroupModels GroupName { get; set; }
    }
}