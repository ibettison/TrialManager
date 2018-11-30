using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class TrialTypeModels
    {
        [Key]
        public int Id { get; set; }
        public string TrialTypeName { get; set; }
        public DateTime? Deleted { get; set; }
    }

}