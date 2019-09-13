using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TrialManager.Models;

namespace TrialManager.Models
{
    public class SettingsModels
    {
        [Key]
        public int Id { get; set; }
        public int  ContactId { get; set; }
        public string HideClosed { get; set; }
        public string IconType { get; set; }
        public string QuickLinks { get; set; }
        public string MiniMenu { get; set; }
        public string ShowDashStages { get; set; }
        
        [ForeignKey("ContactId")]
        public virtual ContactsModels ContactName { get; set; }

    }
}