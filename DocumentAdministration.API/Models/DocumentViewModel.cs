using System;
using System.Collections.Generic;

namespace DocumentAdministration.API.Models
{
    public class DocumentViewModel
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public List<KeywordDetails> Keywords { get; set; }
    }

    public class KeywordDetails
    {
        public Guid? KeywordId { get; set; }
        public string Text { get; set; }
    }
}
