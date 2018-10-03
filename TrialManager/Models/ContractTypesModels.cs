using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class ContractTypesModels
    {
        [Key]
        public int Id { get; set; }
        public string ContractTypeName { get; set; }
    }
}