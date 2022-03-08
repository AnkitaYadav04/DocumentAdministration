using AutoMapper;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Models;

namespace DocumentAdministration.API.Mapper
{
    public class KeywordDetailViewModelProfile:Profile
    {
        public KeywordDetailViewModelProfile()
        {
            CreateMap<DocumentKeywordDetail, KeywordDetailViewModel>();
        }
    }
}
