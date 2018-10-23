using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class HomeViewModels
    {
        public string ShortName { get; set; }
        public string TrialTypeName { get; set; }
        public string Commercial { get; set; }
        public string ProjectId { get; set; }
        public string ResearchId { get; set; }
        public string CI { get; set; }
        public string PI { get; set; }
        public Boolean Access { get; set; }
        public Boolean ReadOnly { get; set; }
        public int ReminderCount { get; set; }
        public int TrialId { get; set; }
    }
}