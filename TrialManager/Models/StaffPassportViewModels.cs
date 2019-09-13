using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TrialManager.Models;

namespace TrialManager.Models
{
    public class StaffPassportViewModels
    {
        public int Id { get; set; }
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [DisplayName("Contract Type")]
        public string ContractType { get; set; }
        [DisplayName("Contract End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ContractEndDate { get; set; }
        [DisplayName("Passport Expiry")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ResearchPassportExpiry { get; set; }
        [DisplayName("Passport Renewal")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ResearchPassportRenewal { get; set; }
        [DisplayName("Access type")]
        public string TypeofAccess{ get; set; }
        [DisplayName("Access Expiry")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AccessExpiryDate { get; set; }
        public int Renewal90 { get; set; }
        public int Renewal180 { get; set; }
    }
}