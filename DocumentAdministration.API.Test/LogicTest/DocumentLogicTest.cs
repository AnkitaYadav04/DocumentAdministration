using AutoMapper;
using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data.DTO;
using DocumentAdministration.API.Logic.Implementation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentAdministration.API.Test.LogicTest
{
    public class DocumentLogicTest
    {
        [Fact]
        public async Task DocumentLogic_GetDocuments_NoRecord_ShouldReturnEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IDocumentRepository>();
            mockRepo.Setup(repo =>repo.GetDocumentKeywordDetails (It.IsAny<string>()))
                .ReturnsAsync(new List<DocumentKeywordDetailsDTO>());

            var mockMapper = new Mock<IMapper>();

            var logic = new DocumentLogic(mockRepo.Object);

            // Act
            var result = await logic.GetDocumentDetailsAsync(It.IsAny<string>());

            // Assert
            Assert.Empty(result);

        }

        [Fact]
        public async Task DocumentLogic_GetDocuments_RecordExist_ShouldReturnEmptyList()
        {
            var mockDocumentData = new List<DocumentKeywordDetailsDTO>()
            {
                new DocumentKeywordDetailsDTO() 
                { 
                    DocumentId = new Guid("6be0839e-78fb-4488-ba8a-243d0915843c"),
                    Name = "Keyboard",
                    KeywordId = Guid.NewGuid(),
                    KeywordText= "Test2022"
                },
                 new DocumentKeywordDetailsDTO()
                {
                    DocumentId = new Guid("6be0839e-78fb-4488-ba8a-243d0915843c"),
                    Name = "Keyboard",
                     KeywordId = Guid.NewGuid(),
                    KeywordText= "Test2021"
                }
            };
            // Arrange
            var mockRepo = new Mock<IDocumentRepository>();
            mockRepo.Setup(repo => repo.GetDocumentKeywordDetails(It.IsAny<string>()))
                .ReturnsAsync(mockDocumentData);

            var mockMapper = new Mock<IMapper>();

            var logic = new DocumentLogic(mockRepo.Object);

            // Act
            var result = await logic.GetDocumentDetailsAsync(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Equal(new Guid("6be0839e-78fb-4488-ba8a-243d0915843c"), result.FirstOrDefault().DocumentId);
            Assert.Equal(2, result.FirstOrDefault().Keywords.Count);

        }

    }
}
