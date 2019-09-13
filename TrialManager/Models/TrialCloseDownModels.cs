using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TrialManager.Models;

namespace TrialManager.Models
{
    public class TrialCloseDownModels
    {
        [Key]
        public int Id { get; set; }
        public int TrialId { get; set; }
        [DisplayName("Clinical Study Report Due")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ClinicalStudyReportDue { get; set; }
        [DisplayName("Clinical Study Report Submitted")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ClinicalStudyReportSubmitted { get; set; }
        [DisplayName("EudraCT Upload Due")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EudraCTUploadDue { get; set; }
        [DisplayName("EudraCT Upload Submitted")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EudraCTUpload { get; set; }
        [DisplayName("Archiving Started")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Archiving { get; set; }
        [DisplayName("Archiving Period")]
        public string ArchivingPeriod { get; set; }
        [DisplayName("Destruction Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Destruction { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}