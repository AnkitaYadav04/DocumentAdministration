using DocumentAdministration.API.Controllers;
using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Models;
using DocumentAdministration.API.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentAdministration.API.Test.ControllerTest
{
    public  class KeywordsControllerTest
    {

        [Fact]
        public async Task KeywordsController_GetKeywordsById_NoRecord_ShouldReturnEmptyList()
        {
            var mockKeywordRespone = new KeywordDetailViewModel() { KeywordId = new Guid(), keyword = "NewYear2022" };
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.GetKeywordDetail(It.IsAny<Guid>()))
                .ReturnsAsync(mockKeywordRespone);
            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.GetKeywordDetails(It.IsAny<Guid>());

            // Assert
            var objectResponse = Assert.IsType<ActionResult<KeywordDetailViewModel>>(result);
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(objectResponse.Result);
            var response = Assert.IsAssignableFrom<KeywordDetailViewModel>(objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotNull(response);

        }

        [Fact]
        public async Task KeywordsController_GetKeywordsById_RecordExist_ShouldReturnNotFoundResult()
        {
            KeywordDetailViewModel keywordDetailViewModel = null;
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.GetKeywordDetail(It.IsAny<Guid>()))
                .ReturnsAsync(keywordDetailViewModel);

            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.GetKeywordDetails(It.IsAny<Guid>());

            // Assert
            var objectResponse = Assert.IsType<ActionResult<KeywordDetailViewModel>>(result);
            var objectResult = Assert.IsAssignableFrom<NotFoundResult>(objectResponse.Result);
            Assert.Equal(404, objectResult.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_AddKeyword_EmptyDocumentId_ShouldReturnBadRequestResult()
        {
            var keywordRequest = new KeywordRequest
            {
                 DocumentId = default(Guid),
                 Keyword = "NewKeyword"
            };
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.AddKeywordDetails(It.IsAny<KeywordRequest>()))
                .ReturnsAsync(new Guid());

            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.AddKeyword(keywordRequest);

            // Assert
            var objectResponse = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_AddKeyword_EmptyKeyword_ShouldReturnBadRequestResult()
        {
            var keywordRequest = new KeywordRequest
            {
                DocumentId = default(Guid),
                Keyword = ""
            };
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.AddKeywordDetails(It.IsAny<KeywordRequest>()))
                .ReturnsAsync(new Guid());

            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.AddKeyword(keywordRequest);

            // Assert
            var objectResponse = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_AddKeyword_ValidKeywordDetails_ShouldReturnCreateResult()
        {
            var keywordRequest = new KeywordRequest
            {
                DocumentId = Guid.NewGuid(),
                Keyword = "MY2022"
            };
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.AddKeywordDetails(It.IsAny<KeywordRequest>()))
                .ReturnsAsync(new Guid());

            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.AddKeyword(keywordRequest);

            // Assert
            var objectResponse = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_UpdateKeyword_InvalidKeywordId_ShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.UpdateKeywordDetails(It.IsAny<Guid>(), It.IsAny<string>())).Verifiable();
                

            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.UpdateKeyword(default(Guid),"TestKeyword");

            // Assert
            var objectResponse = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_UpdateKeyword_InvalidKeyword_ShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.UpdateKeywordDetails(It.IsAny<Guid>(), It.IsAny<string>())).Verifiable();


            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.UpdateKeyword(new Guid(), "TestKeyword");

            // Assert
            var objectResponse = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_UpdateKeyword_ValidData_ShouldReturnNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.UpdateKeywordDetails(It.IsAny<Guid>(), It.IsAny<string>())).Verifiable();


            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.UpdateKeyword(Guid.NewGuid(), "TestKeyword");

            // Assert
            var objectResponse = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_DeleteKeyword_ValidData_ShouldReturnNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.DeleteKeywordDetails(It.IsAny<Guid>())).Verifiable();


            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteKeyword(Guid.NewGuid());

            // Assert
            var objectResponse = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, objectResponse.StatusCode);

        }

        [Fact]
        public async Task KeywordsController_DeleteKeyword_InvalidKeywordId_ShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IKeywordLogic>();
            mockRepo.Setup(logic => logic.DeleteKeywordDetails(It.IsAny<Guid>())).Verifiable();


            var controller = new KeywordsController(mockRepo.Object);

            // Act
            var result = await controller.DeleteKeyword(default(Guid));

            // Assert
            var objectResponse = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, objectResponse.StatusCode);

        }


    }
}
