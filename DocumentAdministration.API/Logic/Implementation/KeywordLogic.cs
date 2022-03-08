using AutoMapper;
using DocumentAdministration.API.Core.Exceptions;
using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Models;
using DocumentAdministration.API.Models.Request;
using System;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Logic.Implementation
{
    public class KeywordLogic : IKeywordLogic
    {
        private readonly IDocumentKeywordDetailsRepository _keywordDetailsRepository;
        private readonly IMapper _mapper;
        public KeywordLogic(IDocumentKeywordDetailsRepository keywordDetailsRepository, IMapper mapper)
        {
            _keywordDetailsRepository = keywordDetailsRepository;
            _mapper = mapper;

        }

        public async Task<KeywordDetailViewModel> GetKeywordDetail(Guid keywordId)
        {
            var response = await _keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId);

            return _mapper.Map<KeywordDetailViewModel>(response);

        }

        public async Task<Guid> AddKeywordDetails(KeywordRequest request)
        {
            var keywordData = await _keywordDetailsRepository.GetDocumentKeywordDetailsById(request.DocumentId, request.Keyword);
            if (keywordData != null)
                throw new ValidationException(System.Net.HttpStatusCode.BadRequest, ErrorMessage.KeywordAlreadyExist);

            var keywordDetails = new DocumentKeywordDetail
            {
                DocumentId = request.DocumentId,
                Keyword = request.Keyword.ToUpperInvariant(),

            };

            return await _keywordDetailsRepository.AddKeywordDetails(keywordDetails);
        }

        public async Task DeleteKeywordDetails(Guid keywordId)
        {
            var keywordData = await _keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId);
            if (keywordData == null)
                throw new ValidationException(System.Net.HttpStatusCode.BadRequest, ErrorMessage.RecordNotFound);

            await _keywordDetailsRepository.DeleteKeywordDetails(keywordData);
        }

        public async Task UpdateKeywordDetails(Guid keywordId, string keyword)
        {
            var keywordData = await _keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId);
            if (keywordData == null)
                throw new ValidationException(System.Net.HttpStatusCode.BadRequest, ErrorMessage.RecordNotFound);

            keywordData.Keyword = keyword;

            await _keywordDetailsRepository.UpdateKeywordDetails(keywordData);
        }
    }
}
