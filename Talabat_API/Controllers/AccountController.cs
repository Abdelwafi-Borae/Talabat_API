using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Data.ModelViews;
using Data.Repositories.Account;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceAccount<DtoRegister> _register;
        private readonly IServiceAccount<DtoLogin> _login;
        public AccountController(IServiceAccount<DtoRegister> register, IServiceAccount<DtoLogin> login)
        {
            _register = register;
            _login = login;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DtoRegister register)
        {
            if (ModelState.IsValid)
            {
                ModelError modelError = await _register.Add(register);
                if (!modelError.IsError)
                {
                    // user register success
                    return Ok(register);
                }else if(modelError.IsError && modelError.identityErrors == null)
                {
                    ModelState.AddModelError("Error", modelError.Message??"Error");
                }
                else
                {
                    foreach (var item in modelError.identityErrors!)
                    {
                        ModelState.AddModelError("Error", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(DtoLogin login)
        {
            if (ModelState.IsValid)
            {
                ModelError modelError = await _login.Add(login);
                if (!modelError.IsError)
                {
                    // user register success
                    return Ok(modelError);
                }
                else if (modelError.IsError && modelError.identityErrors == null)
                {
                    ModelState.AddModelError("Error", modelError.Message ?? "Error");
                }
                else
                {
                    foreach (var item in modelError.identityErrors!)
                    {
                        ModelState.AddModelError("Error", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

    }
}
