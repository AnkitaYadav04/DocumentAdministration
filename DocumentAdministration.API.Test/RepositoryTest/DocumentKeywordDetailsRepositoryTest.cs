using DocumentAdministration.API.Data;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentAdministration.API.Test.RepositoryTest
{
    public class DocumentKeywordDetailsRepositoryTest
    {
        [Fact]
        public void DocumentKeywordDetailsRepository_AddKeywordDetails_ValidData_RecordShouldBeAddedSuccessfully()
        {
            var helper = new DbContextHelper();

            var mockRequest = new DocumentKeywordDetail()
            {
                DocumentId = new Guid("c95079acefcd4c58b1fc43ddd62a49ec"),
                Keyword = "DELL2020"
            };

            var keywordDetailsRepository = helper.GetInMemoryDocumentKeywordDetailsRepository();
         
            var result = keywordDetailsRepository.AddKeywordDetails(mockRequest).GetAwaiter();

            var response = keywordDetailsRepository.GetDocumentKeywordDetailsById(result.GetResult()).Result;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(new Guid("c95079acefcd4c58b1fc43ddd62a49ec"), response.DocumentId);
            Assert.Equal("DELL2020", response.Keyword);
           
        }

        [Fact]
        public async Task DocumentKeywordDetailsRepository_UpdateKeywordDetails_ValidData_RecordShouldBeUpdateSuccessfully()
        {
            var helper = new DbContextHelper();
            var keywordId = new Guid("cc45c08d995740ebb83169c1ba852f9f");

            var keywordDetailsRepository = helper.GetInMemoryDocumentKeywordDetailsRepository();

            var response = keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId).Result;
            response.Keyword = "DELLNEW50Updated";

            await keywordDetailsRepository.UpdateKeywordDetails(response);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("DELLNEW50Updated", response.Keyword);

        }

        [Fact]
        public async Task DocumentKeywordDetailsRepository_DeleteKeywordDetails_ValidData_RecordShouldBeDeletedSuccessfully()
        {
            var helper = new DbContextHelper();
            var keywordId = new Guid("cc45c08d995740ebb83169c1ba852f9f");

            var keywordDetailsRepository = helper.GetInMemoryDocumentKeywordDetailsRepository();

            var responseBeforeDelete = keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId).Result;

            Assert.NotNull(responseBeforeDelete);

            await keywordDetailsRepository.DeleteKeywordDetails(responseBeforeDelete);

            var responseAfterDelete = keywordDetailsRepository.GetDocumentKeywordDetailsById(keywordId).Result;

            // Assert
            Assert.Null(responseAfterDelete);
        

        }

    }
}
