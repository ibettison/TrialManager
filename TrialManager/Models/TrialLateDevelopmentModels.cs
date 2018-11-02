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
    public class TrialLateDevelopmentModels
    {
        [Key]
        public int Id { get; set; }
        public int TrialId { get; set; }
        [DisplayName("Clinical Trials.Gov Reference")]
        public string ClinicalTrialsGovRef { get; set; }
        [DisplayName("Local R&D C&C")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LocalCC { get; set; }
        [DisplayName("Sponsor Green light")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SponsorGreenLight { get; set; }
        [DisplayName("Site Initiation Visit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SiteInitiationVisit { get; set; }
        [DisplayName("Local Site Activation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LocalSiteActivationDate { get; set; }

        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}