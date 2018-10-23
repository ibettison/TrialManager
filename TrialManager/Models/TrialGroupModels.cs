using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialGroupModels
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}