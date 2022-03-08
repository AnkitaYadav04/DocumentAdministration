using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentAdministration.API.Data.Entity
{
    public class DocumentKeywordDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid KeywordId { get; set; }
        public Guid DocumentId { get; set; }
        public string Keyword { get; set; }
        public Document Document { get; set; } 
    }
}
