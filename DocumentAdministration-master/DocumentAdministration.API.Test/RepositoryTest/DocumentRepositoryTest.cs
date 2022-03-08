using DocumentAdministration.API.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocumentAdministration.API.Test.RepositoryTest
{
    public class DocumentRepositoryTest
    {

        [Fact]
        public void DocumentRepository_GetDocumentsKeywordDetails_RecordExist_ShouldReturnData()
        {
            var helper = new DbContextHelper();

            var documentRepository = helper.GetInMemoryDocumentRepository();

            var response = documentRepository.GetDocumentKeywordDetails("").Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Count);

        }

        [Fact]
        public void DocumentRepository_GetDocumentKeywordDetails_RecordExist_ShouldReturnData()
        {
            var helper = new DbContextHelper();

            var documentRepository = helper.GetInMemoryDocumentRepository();

            var response = documentRepository.GetDocumentKeywordDetails(new Guid("dea1d4cfd13f4fa2994b2207078ae0bf")).Result;

            // Assert
            Assert.NotNull(response);
            Assert.Single(response);

        }
    }
}
