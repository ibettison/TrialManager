﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class ContractTypesModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Contract Type")]
        public string ContractTypeName { get; set; }

        public DateTime? Deleted { get; set; }
    }
}