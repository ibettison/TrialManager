using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class RolesModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Role")]
        public string RoleName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}