using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trialmanager.Models
{
    public class TrialDocumentsModels
    {

        [Key]
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        public string UploadedBy { get; set; }
        public string DocumentLink { get; set; }
        public string DocumentFileName { get; set; }
        public string DocumentVersion { get; set; }
        public int DocumentType { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFile { get; set; }
        public string DocumentDescription { get; set; }
        public int TrialId { get; set; }

        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }

        [ForeignKey("DocumentType")]
        public virtual DocumentTypesModels TypeName { get; set; }
        

    }
}