using AutoMapper;
using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Logic.Implementation;
using DocumentAdministration.API.Models;
using DocumentAdministration.API.Models.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentAdministration.API.Test.LogicTest
{
    public class KeywordLogicTest
    {
        [Fact]
        public async Task KeywordLogic_GetKeywordDetail_NoRecord_ShouldReturnEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IDocumentKeywordDetailsRepository>();
            mockRepo.Setup(repo => repo.GetDocumentKeywordDetailsById(It.IsAny<Guid>()))
                .ReturnsAsync((DocumentKeywordDetail)null);

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<KeywordDetailViewModel>(It.IsAny<DocumentKeywordDetail>()))
            .Returns((KeywordDetailViewModel)null);

            var logic = new KeywordLogic(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await logic.GetKeywordDetail(It.IsAny<Guid>());

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public async Task KeywordLogic_GetKeywordDetail_RecordExist_ShouldReturnKeywordDetails()
        {
            var mockKeywordDetails = new DocumentKeywordDetail
            {
                Keyword = "NY@022",
                KeywordId = new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"),

            };

            var mockKeywordViewModel = new KeywordDetailViewModel
            {
                KeywordId = new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"),
                keyword = "NY@022",

            };
            // Arrange
            var mockRepo = new Mock<IDocumentKeywordDetailsRepository>();
            mockRepo.Setup(repo => repo.GetDocumentKeywordDetailsById(It.IsAny<Guid>()))
                .ReturnsAsync(mockKeywordDetails);

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<KeywordDetailViewModel>(It.IsAny<DocumentKeywordDetail>()))
            .Returns(mockKeywordViewModel);

            var logic = new KeywordLogic(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await logic.GetKeywordDetail(It.IsAny<Guid>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"), result.KeywordId);
            Assert.Equal("NY@022", result.keyword);


        }

        [Fact]
        public async Task KeywordLogic_AddKeywordDetails_ValidData_ShouldReturnKeywordID()
        {
            var mockKeywordRequest = new KeywordRequest
            {
                DocumentId = new Guid(),
                Keyword =  "Test"
            };
            // Arrange
            var mockRepo = new Mock<IDocumentKeywordDetailsRepository>();
            mockRepo.Setup(repo => repo.AddKeywordDetails(It.IsAny<DocumentKeywordDetail>()))
                .ReturnsAsync(new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"));

            mockRepo.Setup(repo => repo.GetDocumentKeywordDetailsById(It.IsAny<Guid>()))
               .ReturnsAsync((DocumentKeywordDetail)null);

            var mockMapper = new Mock<IMapper>();


            var logic = new KeywordLogic(mockRepo.Object, mockMapper.Object);

            // Act
            var result = await logic.AddKeywordDetails(mockKeywordRequest);

            // Assert
          
            Assert.Equal( new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"), result);

        }

        [Fact]
        public async Task KeywordLogic_UpdateKeywordDetails_ValidData_KeywordShouldBeUpdate()
        {
            var mockKeywordRequest = new DocumentKeywordDetail
            {
                KeywordId = new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"),
                Keyword = "Test"
            };
            // Arrange
            var mockRepo = new Mock<IDocumentKeywordDetailsRepository>();
            mockRepo.Setup(repo => repo.UpdateKeywordDetails(It.IsAny<DocumentKeywordDetail>())).Verifiable();
                

            mockRepo.Setup(repo => repo.GetDocumentKeywordDetailsById(It.IsAny<Guid>()))
               .ReturnsAsync(mockKeywordRequest);

            var mockMapper = new Mock<IMapper>();


            var logic = new KeywordLogic(mockRepo.Object, mockMapper.Object);

            // Act
            await logic.UpdateKeywordDetails(mockKeywordRequest.KeywordId, "TestUpdate");

            // Assert

            Assert.Equal("TestUpdate", mockKeywordRequest.Keyword);

        }

        [Fact]
        public async Task KeywordLogic_DeleteKeywordDetails_ValidData_KeywordShouldBeDeleted()
        {
            var mockKeywordRequest = new DocumentKeywordDetail
            {
                KeywordId = new Guid("05eb0332-30f9-4cd5-b897-feef874f4ab4"),
                Keyword = "DeleteTest"
            };
            // Arrange
            var mockRepo = new Mock<IDocumentKeywordDetailsRepository>();
          
            mockRepo.Setup(repo => repo.GetDocumentKeywordDetailsById(It.IsAny<Guid>()))
               .ReturnsAsync(mockKeywordRequest);

            var mockMapper = new Mock<IMapper>();


            var logic = new KeywordLogic(mockRepo.Object, mockMapper.Object);

            // Act
            await logic.DeleteKeywordDetails(mockKeywordRequest.KeywordId);

            // Assert

            mockRepo.Setup(repo => repo.DeleteKeywordDetails(It.IsAny<DocumentKeywordDetail>())).Verifiable(); ;

        }

    }
}
