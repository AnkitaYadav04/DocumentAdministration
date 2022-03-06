using DocumentAdministration.API.Data.Entity;
using System;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Core.Interfaces.Database
{
    public interface IDocumentKeywordDetailsRepository
    {
        Task<Guid> AddKeywordDetails(DocumentKeywordDetail documentKeywordDetail);
        Task DeleteKeywordDetails(DocumentKeywordDetail documentKeywordDetail);
        Task UpdateKeywordDetails(DocumentKeywordDetail documentKeywordDetail);
        Task<DocumentKeywordDetail> GetDocumentKeywordDetailsById(Guid keywordId);
        Task<DocumentKeywordDetail> GetDocumentKeywordDetailsById(Guid documentId, string Text);
    }
}
