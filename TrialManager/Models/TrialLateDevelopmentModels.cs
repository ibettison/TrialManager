using System;
using System.Collections.Generic;
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
        public string ClinicalTrialsGovRef { get; set; }
        public DateTime LocalCC { get; set; }
        public DateTime SponsorGreenLight { get; set; }
        public DateTime LocalSiteInitiation { get; set; }
        public DateTime TargetFpfvDate { get; set; }
        public DateTime MultiSiteInitiation { get; set; }

        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}