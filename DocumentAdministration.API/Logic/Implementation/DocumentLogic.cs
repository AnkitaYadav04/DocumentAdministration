
using AutoMapper;
using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Logic.Implementation
{

    public class DocumentLogic : IDocumentLogic
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentLogic(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentViewModel> GetDocumentDetailsAsync(Guid documentId)
        {
            var responseData = await _documentRepository.GetDocumentKeywordDetails(documentId);
            if (responseData == null) return new DocumentViewModel();

           var  documentResponse = FormateDocumentResponseData(responseData).FirstOrDefault();

            return documentResponse;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetDocumentsDetailsAsync(string filterKeyword = null)
        {

            var responseData = await _documentRepository.GetDocumentKeywordDetails(filterKeyword);
            if (responseData == null) return new List<DocumentViewModel>();

            IEnumerable<DocumentViewModel> documentResponse = FormateDocumentResponseData(responseData);

            return documentResponse;
        }

        private static IEnumerable<DocumentViewModel> FormateDocumentResponseData(List<DocumentKeywordDetailsDTO> responseData)
        {
            return responseData.GroupBy(x => x.DocumentId)?.Select(response =>
                  new DocumentViewModel
                  {
                      DocumentId = response.Key,
                      Name = response.First().Name,
                      Keywords = response.Where(y => y.KeywordId != null).Select(x =>
                      new KeywordDetails
                      {
                          KeywordId = x.KeywordId,
                          Text = x.KeywordText

                      }).ToList()

                  });
        }
    }
}
