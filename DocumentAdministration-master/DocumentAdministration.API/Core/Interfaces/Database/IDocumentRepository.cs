using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Core.Interfaces.Database
{
    public interface IDocumentRepository
    {

        Task<List<DocumentKeywordDetailsDTO>> GetDocumentKeywordDetails(string filterKeyword);
        Task<List<DocumentKeywordDetailsDTO>> GetDocumentKeywordDetails(Guid documentId);


    }
}
