using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class DocumentTypesModels
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Document type")]
        public string TypeName { get; set; }
        public DateTime? Deleted { get; set; }
    }
}