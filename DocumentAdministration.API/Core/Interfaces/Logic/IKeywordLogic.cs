using DocumentAdministration.API.Models;
using DocumentAdministration.API.Models.Request;
using System;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Core.Interfaces.Logic
{
    public interface IKeywordLogic
    {
        Task<KeywordDetailViewModel> GetKeywordDetail(Guid keywordId);
        Task<Guid> AddKeywordDetails(KeywordRequest documentKeywordDetail);
        Task DeleteKeywordDetails(Guid keywordId);
        Task UpdateKeywordDetails(Guid keywordId, string keyword);
    }
}
