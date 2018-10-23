using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Models
{
    public class TrialGroupTrialModels
    {
        [Key]
        public int Id { get; set; }
        public int TrialId { get; set; }
        public int TrialGroupId { get; set; }
        [ForeignKey("TrialGroupId")]
        public virtual TrialGroupModels GroupName { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}