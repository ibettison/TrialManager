using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrialManager.Models
{
    public class TrialFeasibilityModels
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Short Name is required")]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "The Trial Title is required")]
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

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Grant type")]
        [DisplayName("Grant type")]
        public int GrantTypeId { get; set; }

        [DisplayName("Funding Stream")]
        public string FundingStream { get; set; }

        [DisplayName("Grant deadline")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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