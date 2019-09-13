using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialRecordsModels
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Date of Change")]
        public DateTime DateChanged { get; set; }

        [DisplayName("Who made the change")]
        public string WhoChanged { get; set; }

        [DisplayName("Original Value")]
        public string OriginalValue { get; set; }

        [DisplayName("New Value")]
        public string NewValue { get; set; }

        [DisplayName("FieldName")]
        public string FieldName { get; set; }

        [DisplayName("Reason for the change")]
        public string ReasonForChange { get; set; }

        public int TrialId { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
        public string Original { get; internal set; }
    }
}