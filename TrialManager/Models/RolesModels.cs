using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class RolesModels
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}