using Data.Model;
using Data.ModelViews;
using Tawtheiq.Application.Cores.Identity.Dtos.Respones;

namespace Data.Repositories.Account
{
    public interface IIdentityService
    {
     
        public   Task<ApplicationUser> RegisterAsync(DtoRegister register);
        public   Task<TokenResponse> Login(DtoLogin register);
    }
}
