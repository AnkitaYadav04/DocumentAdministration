using System;
using System.Collections.Generic;

namespace DocumentAdministration.API.Models.Request
{
    public class KeywordRequest
    {
        public string Keyword { get; set; } = string.Empty;
        public Guid DocumentId { get; set; }    
    }
}
