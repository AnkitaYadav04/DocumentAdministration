using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentAdministration.API.Data.Entity
{
    public class Document
    {
        public Guid DocumentId { get; set; }
       
        public string Name { get; set; }
        public ICollection<DocumentKeywordDetail> DocumentKeywordDetails { get; set; }

    }
    
}
