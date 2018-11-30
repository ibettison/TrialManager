using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Models
{
    public class TrialActiveModels
    {
        [Key]
        public int Id { get; set; }
        public int TrialId { get; set; }
        [DisplayName("Trial Status")]
        public int StatusId { get; set; }
        [DisplayName("Target FPFV")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TargetFPFV { get; set; }
        [DisplayName("Actual FPFV")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActualFPFV { get; set; }
        [DisplayName("Current Recruitment")]
        public string CurrentRecruitment { get; set; }
        [DisplayName("Estimated LPLV")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EstimatedLPLV { get; set; }
        [DisplayName("Actual LPLV")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActualLPLV { get; set; }
        [DisplayName("End of Study Notification Due")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EOSNotificationDue { get; set; }
        [DisplayName("End of Study Notification")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EOSNotificationSubmitted { get; set; }
        [DisplayName("Site Close Down Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SiteCloseDown { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
        [ForeignKey("StatusId")]
        public virtual ActiveStatusModels StatusName { get; set; }
    }
}