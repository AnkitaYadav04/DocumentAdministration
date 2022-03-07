using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Data.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentAdministrationDbContext _dbContext;
        public DocumentRepository(DocumentAdministrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DocumentKeywordDetailsDTO>> GetDocumentKeywordDetails(string filterKeyword)
        {
            
            var documentKeywordDetails = await (from document in _dbContext.Documents
                         join keywordDetail in _dbContext.DocumentKeywordDetails on document.DocumentId equals keywordDetail.DocumentId into Details
                         from keywordDetails in Details.DefaultIfEmpty()
                         where (string.IsNullOrWhiteSpace(filterKeyword) || keywordDetails.Keyword.Contains(filterKeyword.Trim().ToUpperInvariant()))
                         orderby document.Name
                         select 
                         new DocumentKeywordDetailsDTO
                         {
                             DocumentId = document.DocumentId,
                             KeywordId = keywordDetails.KeywordId,
                             KeywordText = keywordDetails.Keyword,
                             Name = document.Name,

                         }).ToListAsync();

            return documentKeywordDetails;  
        }

        public async Task<List<DocumentKeywordDetailsDTO>> GetDocumentKeywordDetails(Guid documentId)
        {
            var documentKeywordDetails = await(from document in _dbContext.Documents
                                               join keywordDetail in _dbContext.DocumentKeywordDetails on document.DocumentId equals keywordDetail.DocumentId into Details
                                               from keywordDetails in Details.DefaultIfEmpty()
                                               where document.DocumentId == documentId
                                               orderby document.Name
                                               select
                                               new DocumentKeywordDetailsDTO
                                               {
                                                   DocumentId = document.DocumentId,
                                                   KeywordId = keywordDetails.KeywordId,
                                                   KeywordText = keywordDetails.Keyword,
                                                   Name = document.Name,

                                               }).ToListAsync();

            return documentKeywordDetails;
        }
    }
}
