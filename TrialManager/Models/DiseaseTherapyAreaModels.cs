using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace TrialManager.Models
{
    public class DiseaseTherapyAreaModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Disease Therapy Area")]
        public string DiseaseTherapyAreaName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}