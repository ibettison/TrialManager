using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class TrialLocationModels
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Trial Location")]
        public string Location { get; set; }

        public DateTime? Deleted { get; set; }
    }
}