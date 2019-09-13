using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialSetupModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Project Id")]
        public string ProjectIdentifier { get; set; }
        [DisplayName("R&D Id")]
        public string ResearchDevelopmentId { get; set; }
        [DisplayName("Grant Funder Reference")]
        public string GrantFunderRef { get; set; }
        [DisplayName("Sponsor Reference")]
        public string SponsorRef { get; set; }
        [DisplayName("REC Reference")]
        public string RecRef { get; set; }
        [DisplayName("EUDRACT Reference")]
        public string EudractRef { get; set; }
        [DisplayName("IRAS Reference")]
        public string IrasId { get; set; }
        [DisplayName("Recruitment Target")]
        public string RecruitmentTarget { get; set; }
        [DisplayName("Trial Location")]
        public int TrialLocationId { get; set; }
        [DisplayName("Site Name")]
        public string SiteName { get; set; }
        [DisplayName("Local Approval")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ApprovalDate { get; set; }
        [DisplayName("REC Approval")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? RecDate { get; set; }
        [DisplayName("HRA Approval")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? HraDate { get; set; }
        [DisplayName("CTA Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CtaDate { get; set; }
        [DisplayName("Funder Milestone Report Submission")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? FunderMileStoneReport { get; set; }
        public int TrialId { get; set; }

        [ForeignKey("TrialLocationId")]
        public virtual TrialLocationModels Location { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}