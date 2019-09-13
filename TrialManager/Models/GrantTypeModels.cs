using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class GrantTypeModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Grant type")]
        public string GrantTypeName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}