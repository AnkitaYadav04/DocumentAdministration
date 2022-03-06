using DocumentAdministration.API.Controllers;
using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentAdministration.API.Test.ControllerTest
{

    public class DocumentControllerTest
    {

        [Fact]
        public async Task DocumentController_GetDocuments_NoRecord_ShouldReturnEmptyList()
        {
            // Arrange
            var mockRepo = new Mock<IDocumentLogic>();
            mockRepo.Setup(logic => logic.GetDocumentDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<DocumentViewModel>());
            var controller = new DocumentsController(mockRepo.Object);

            // Act
            var result = await controller.GetDocuments(It.IsAny<string>());

            // Assert
            var objectResponse = Assert.IsType<ActionResult<List<DocumentViewModel>>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(objectResponse.Result);
            var response = Assert.IsAssignableFrom<List<DocumentViewModel>>(objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Empty(response);

        }

        [Fact]
        public async Task DocumentController_GetDocuments_RecordInDB_ShouldReturnDocumentList()
        {
            var mockDocumentList = new List<DocumentViewModel>()
            {
                 new DocumentViewModel()
                 {
                    DocumentId =new Guid("eb5eb2f0-df45-4d2f-aa6a-2efb8cdbeb5c"),
                    Name =  "Laptop",
                    Keywords = new List<KeywordDetails>
                    {
                      new KeywordDetails()
                      {
                        KeywordId = new Guid("9788dfa0-1b3a-4183-b33c-ee7fdfb4db24"),
                        Text ="MY2022"
                      }
                    }
                 },
                  new DocumentViewModel()
                  {
                    DocumentId =new Guid("eb5eb2f0-df45-4d2f-aa6a-2efb8cdbeb5c"),
                    Name =  "Laptop",
                    Keywords = new List<KeywordDetails>()
                    
                  }

            };

            // Arrange
            var mockRepo = new Mock<IDocumentLogic>();
            mockRepo.Setup(logic => logic.GetDocumentDetailsAsync(It.IsAny<string>()))
                .ReturnsAsync(mockDocumentList);
            var controller = new DocumentsController(mockRepo.Object);

            // Act
            var result = await controller.GetDocuments(It.IsAny<string>());

            // Assert
            var objectResponse = Assert.IsType<ActionResult<List<DocumentViewModel>>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(objectResponse.Result);
            var response = Assert.IsAssignableFrom<List<DocumentViewModel>>(objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(2, response.Count);

        }
    }
}
