using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class TrialsModels
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string TrialTitle { get; set; }

    }
}