using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Trialmanager.Models;

namespace TrialManager.Models
{
    public class RecentTrialsModels
    {
        public int Id { get; set; }
        public int TrialId { get; set; }
        public  DateTime  LastAccessed { get; set; }
        public string UserId { get; set; }
        public string Access { get; set; }
        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }
    }
}