using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class GrantTypeModels
    {
        [Key]
        public int Id { get; set; }
        public string GrantTypeName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}