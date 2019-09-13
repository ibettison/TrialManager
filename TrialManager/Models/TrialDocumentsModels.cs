using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrialManager.Models
{
    public class TrialDocumentsModels
    {

        [Key]
        public int Id { get; set; }

        public DateTime  DateTime  { get; set; }
        [DisplayName("Uploaded by")]
        public string UploadedBy { get; set; }
        [DisplayName("Document Link")]
        public string DocumentLink { get; set; }
        [DisplayName("Document Filename")]
        public string DocumentFileName { get; set; }
        [DisplayName("Version")]
        public string DocumentVersion { get; set; }
        [DisplayName("Document type")]
        public int DocumentType { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadFile { get; set; }
        [DisplayName("Document Description")]
        public string DocumentDescription { get; set; }
        public int TrialId { get; set; }

        [ForeignKey("TrialId")]
        public virtual TrialFeasibilityModels ShortName { get; set; }

        [ForeignKey("DocumentType")]
        public virtual DocumentTypesModels TypeName { get; set; }
        

    }
}