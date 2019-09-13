using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialTypeModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Trial Type")]
        public string TrialTypeName { get; set; }
        public DateTime? Deleted { get; set; }
    }

}