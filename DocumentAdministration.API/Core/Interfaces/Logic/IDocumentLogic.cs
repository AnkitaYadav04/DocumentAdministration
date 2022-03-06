using DocumentAdministration.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Core.Interfaces.Logic
{
    public interface IDocumentLogic
    {
        Task<IEnumerable<DocumentViewModel>> GetDocumentDetailsAsync(string filterKeyword = null);
    }
}
