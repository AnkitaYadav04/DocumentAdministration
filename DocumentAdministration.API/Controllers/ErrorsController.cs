using DocumentAdministration.API.Core.Exceptions;
using DocumentAdministration.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAdministration.API.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorViewModel Error()
        
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; 
            var code = 500; // Internal Server Error by default

            if (exception is ValidationException) code = 400; 
           

            Response.StatusCode = code;

            return new ErrorViewModel(exception); 
        }
    }
}
