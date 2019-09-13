using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class StaffPassportModels
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        [DisplayName("Contract Type")]
        public int ContractTypeId { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Contract End Date")]
        public DateTime ContractEndDate { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Passport Expiry")]
        public DateTime ResearchPassportExpiry { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Passport Renewal")]
        public DateTime ResearchPassportRenewal { get; set; }
        [DisplayName("Access type")]
        public int TypeofAccessId { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Access Expiry")]
        public DateTime AccessExpiryDate { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactsModels ContactName { get; set; }
        [ForeignKey("TypeofAccessId")]
        public virtual AccessTypesModels AccessName { get; set; }
        [ForeignKey("ContractTypeId")]
        [DisplayName("Contract Type")]
        public virtual ContractTypesModels ContractTypeName { get; set; }
    }
}