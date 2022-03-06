using DocumentAdministration.API.Core.Interfaces.Logic;
using DocumentAdministration.API.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DocumentAdministration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordLogic _KeywordLogic;
        public KeywordsController(IKeywordLogic KeywordLogic)
        {
            _KeywordLogic = KeywordLogic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",Name = "GetKeywordDetails")]
        public async Task<IActionResult> GetKeywordDetails(Guid id )
        {
            var response = await _KeywordLogic.GetKeywordDetail(id);

            return Ok(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywordRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddKeywordDetails")]
        public async Task<ActionResult> AddKeyword([FromBody] KeywordRequest keywordRequest)
        {
            if (keywordRequest.DocumentId == default(Guid) || string.IsNullOrEmpty(keywordRequest.Keyword))
                return BadRequest();

            var keywordId = await _KeywordLogic.AddKeywordDetails(keywordRequest);

             return CreatedAtAction(nameof(GetKeywordDetails), new { id = keywordId }, keywordRequest);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">keywordId</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "UpdateKeywordDetails")]
        public async Task<ActionResult> UpdateKeyword(Guid id ,[FromBody] string keyword )
        {
            if (id == default(Guid) || string.IsNullOrEmpty(keyword)) return BadRequest();

            await _KeywordLogic.UpdateKeywordDetails(id, keyword);

            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">keywordId</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteKeyword")]
        public async Task<ActionResult> DeleteKeyword(Guid id)
        {
            if (id == default(Guid)) return BadRequest();

            await _KeywordLogic.DeleteKeywordDetails(id);

            return NoContent();
        }
    }
}
