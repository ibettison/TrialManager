using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class StaffPassportModels
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int TypeofAccessId { get; set; }
        public DateTime AccessExpiryDate { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactsModels ContactName { get; set; }
        [ForeignKey("TypeofAccessId")]
        public virtual AccessTypesModels AccessName { get; set; }
        [ForeignKey("ContractTypeId")]
        public virtual ContractTypesModels ContractTypeName { get; set; }
    }
}