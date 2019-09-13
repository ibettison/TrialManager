using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class PhaseModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Phase type")]
        public string PhaseName { get; set; }
        public DateTime? Deleted { get; set; }
    }

}