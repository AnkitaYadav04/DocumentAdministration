using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Data.Repository
{
    public class DocumentKeywordDetailsRepository : IDocumentKeywordDetailsRepository
    {
        private readonly DocumentAdministrationDbContext _dbContext;
        public DocumentKeywordDetailsRepository(DocumentAdministrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> AddKeywordDetails(DocumentKeywordDetail documentKeywordDetail)
        {
            await _dbContext.DocumentKeywordDetails.AddAsync(documentKeywordDetail);
            await _dbContext.SaveChangesAsync();
            return documentKeywordDetail.KeywordId;
        }

        public async Task DeleteKeywordDetails(DocumentKeywordDetail documentKeywordDetail)
        {
             _dbContext.DocumentKeywordDetails.Remove(documentKeywordDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DocumentKeywordDetail> GetDocumentKeywordDetailsById(Guid keywordId)
        {
            return await _dbContext.DocumentKeywordDetails.FirstOrDefaultAsync(x => x.KeywordId == keywordId);
        }
        public async Task<DocumentKeywordDetail> GetDocumentKeywordDetailsById(Guid documentId, string Text)
        {
            Text = Text.Trim().ToUpperInvariant();
            return await _dbContext.DocumentKeywordDetails.FirstOrDefaultAsync(x => x.DocumentId == documentId && x.Keyword == Text.ToUpperInvariant());
        }

        public async Task UpdateKeywordDetails(DocumentKeywordDetail documentKeywordDetail)
        {
            _dbContext.DocumentKeywordDetails.Update(documentKeywordDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
