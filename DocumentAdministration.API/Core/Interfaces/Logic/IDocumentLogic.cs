using DocumentAdministration.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Core.Interfaces.Logic
{
    public interface IDocumentLogic
    {
        Task<IEnumerable<DocumentViewModel>> GetDocumentsDetailsAsync(string filterKeyword = null);
        Task<DocumentViewModel> GetDocumentDetailsAsync(Guid documentId);
    }
}
