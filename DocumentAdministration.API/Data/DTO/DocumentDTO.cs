using System;

namespace DocumentAdministration.API.Data.DTO
{
    public class DocumentKeywordDetailsDTO
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public Guid? KeywordId { get; set; }
        public string KeywordText { get; set; }
    }
}
