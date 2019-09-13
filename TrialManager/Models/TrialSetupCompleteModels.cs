using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TrialManager.Models;

namespace TrialManager.Models
{
    public class TrialSetupCompleteModels
    {
        [Key]
        public int Id { get; set; }
        public string Completed { get; set; }
        public int TrialId { get; set; }
        public string Reason { get; set; }
        public DateTime  DateTime  { get; set; }

        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}