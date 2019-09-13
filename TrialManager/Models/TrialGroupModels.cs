using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialGroupModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Group Name")]
        public string GroupName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}