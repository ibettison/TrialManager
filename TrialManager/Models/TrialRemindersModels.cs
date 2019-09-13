using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialRemindersModels
    {
        [Key]
        public int Id { get; set; }
        public string Reminder { get; set; }
        public int TrialId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DueDate { get; set; }
        public Boolean Checked { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}