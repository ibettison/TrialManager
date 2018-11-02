using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trialmanager.Models
{
    public class TrialFeasibilityModels
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        [DisplayName("Trial title")]
        public string TrialTitle { get; set; }

        [DisplayName("Application reference")]
        public string ApplicationReference { get; set; }

        [DisplayName("BH Number")]
        public string BhNumber { get; set; }

        [DisplayName("Trial type")]
        public int TrialTypeId { get; set; }

        [DisplayName("Commercial")]
        public string Commercial { get; set; }

        [DisplayName("Trial Phase")]
        public int PhaseId { get; set; }

        [DisplayName("Sample Size")]
        public string SampleSize { get; set; }

        [DisplayName("Grant type")]
        public int GrantTypeId { get; set; }

        [DisplayName("Funding Stream")]
        public string FundingStream { get; set; }

        [DisplayName("Grant deadline")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? GrantDeadlineDate { get; set; }

        [DisplayName("University Consultancy Agreement")]
        public bool UniConsultancyAgreement { get; set; }

        [DisplayName("Trust Consultancy Agreement")]
        public bool NhsConsultancyAgreement { get; set; }

        [DisplayName("Disease Therapy Area")]
        public int DiseaseTherapyAreaId { get; set; }

        [ForeignKey("TrialTypeId")]
        public virtual TrialTypeModels TrialTypeName { get; set; }
        [ForeignKey("PhaseId")]
        public virtual PhaseModels PhaseName { get; set; }
        [ForeignKey("GrantTypeId")]
        public virtual GrantTypeModels GrantTypeName { get; set; }
        [ForeignKey("DiseaseTherapyAreaId")]
        public virtual DiseaseTherapyAreaModels DiseaseTherapyAreaName { get; set; }
    }
}