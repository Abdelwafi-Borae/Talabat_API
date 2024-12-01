using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Data.ModelViews;
using Data.Repositories.Account;
using Tawtheiq.Application.Common.Models;
using Data.Model;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _Identity;
         
        public AccountController(IIdentityService Identity)
        {
            _Identity = Identity;
           
        }

        
        [HttpPost("RegisterAsync")]
        [ProducesResponseType(typeof(GenericResult<ModelError>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<GenericResult<ApplicationUser>> RegisterAsync(DtoRegister register)
        {

            var result = await _Identity.RegisterAsync(register);
           
            Response.StatusCode = StatusCodes.Status201Created;
            return result.ToCreatedResult();

            
        }
        [ProducesResponseType(typeof(GenericResult<TokenResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(GenericResult<object>), StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<GenericResult<TokenResponse>> Login(DtoLogin login)
        {
            var Result = await _Identity.Login(login);

            Response.StatusCode = StatusCodes.Status201Created;
            return Result.ToCreatedResult();
            
        }

    }
}
