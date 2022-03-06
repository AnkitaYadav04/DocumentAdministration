using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DocumentAdministration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly IDocumentLogic _documentLogic;
        public DocumentsController(ILogger<DocumentsController> logger, IDocumentLogic documentLogic)
        {
            _logger = logger;
            _documentLogic = documentLogic;
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="filterKeyword"></param>
      /// <returns></returns>
        [HttpGet( Name = "GetDocuments")]
        public async Task<ActionResult<List<DocumentViewModel>>> GetDocuments( string filterKeyword = null)
        {
            var respone = await _documentLogic.GetDocumentDetailsAsync(filterKeyword);

            return Ok(respone);
        }
    }
}
