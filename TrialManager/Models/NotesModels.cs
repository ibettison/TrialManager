using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{

    public class NotesModels
    {
        [Key]
        public int Id { get; set; }
        public string Who { get; set; }
        public DateTime DateTime { get; set; }
        public int TrialId { get; set; }
        public string Message { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}