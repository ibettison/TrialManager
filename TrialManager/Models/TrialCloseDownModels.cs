using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Models
{
    public class TrialCloseDownModels
    {
        [Key]
        public int Id { get; set; }
        public int TrialId { get; set; }
        public DateTime? ClinicalStudyReportDue { get; set; }
        public DateTime? ClinicalStudyReportSubmitted { get; set; }
        public DateTime? EudraCTUploadDue { get; set; }
        public DateTime? EudraCTUpload { get; set; }
        public DateTime? Archiving { get; set; }
        public DateTime? ArchivingPeriod { get; set; }
        public DateTime? Destruction { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}