using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class TrialSetupModels
    {
        [Key]
        public int Id { get; set; }
        public string ProjectIdentifier { get; set; }
        public string ResearchDevelopmentId { get; set; }
        public string GrantFunderRef { get; set; }
        public string SponsorRef { get; set; }
        public string RecRef { get; set; }
        public string EudractRef { get; set; }
        public string IrasId { get; set; }
        public string RecruitmentTarget { get; set; }
        public int TrialLocationId { get; set; }
        public DateTime RecDate { get; set; }
        public DateTime HraDate { get; set; }
        public DateTime CtaDate { get; set; }
        public int TrialId { get; set; }

        [ForeignKey("TrialLocationId")]
        public virtual TrialLocationModels Location { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}