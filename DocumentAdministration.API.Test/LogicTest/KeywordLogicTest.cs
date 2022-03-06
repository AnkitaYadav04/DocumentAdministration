using AutoMapper;
using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Logic.Implementation;
using DocumentAdministration.API.Models;
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
        public async Task KeywordLogic_GetKeywordDetail_RecordExist_ShouldReturnEmptyList()
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
    }
}
