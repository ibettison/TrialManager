using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class AjaxViewModels
    {
        public string Original { get; set; }
        public string FieldName { get; set; }
        public string NewValue { get; set; }
        public string Reason { get; set; }
        public string Controller { get; set; }
        public int TrialId { get; set; }
        public int Id { get; set; }
    }
}