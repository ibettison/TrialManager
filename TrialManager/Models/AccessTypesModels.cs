using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class AccessTypesModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Access type")]
        public string AccessName { get; set; }

        public DateTime? Deleted { get; set; }
    }
}