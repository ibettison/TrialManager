using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class PhaseModels
    {
        [Key]
        public int Id { get; set; }
        public string PhaseName { get; set; }
        public DateTime? Deleted { get; set; }
    }

}