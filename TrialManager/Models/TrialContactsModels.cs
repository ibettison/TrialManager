using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
    {
        public class TrialContactsModels
        {
            [Key]
            public int Id { get; set; }
            public int ContactId { get; set; }
            public int TrialId { get; set; }
            public int RoleId { get; set; }
            public string RoleDescription { get; set; }

            [ForeignKey("TrialId")]
            public virtual TrialFeasibilityModels ShortName { get; set; }

            [ForeignKey("ContactId")]
            public virtual ContactsModels ContactName { get; set; }

            [ForeignKey("RoleId")]
            public virtual RolesModels RoleName { get; set; }

        }
    }