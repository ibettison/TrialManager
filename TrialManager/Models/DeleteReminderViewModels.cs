using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class DeleteReminderViewModels
    {
        public int Id { get; set; }
        public Boolean Delete { get; set; }
        public int TrialId { get; set; }
    }
}