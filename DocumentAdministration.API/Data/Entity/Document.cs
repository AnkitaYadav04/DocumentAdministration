using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentAdministration.API.Data.Entity
{
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DocumentId { get; set; }
       
        public string Name { get; set; }
        public virtual ICollection<DocumentKeywordDetail> DocumentKeywordDetails { get; set; }

    }
    
}
