using Data.Model;
using Data.ModelViews;

namespace Data.Repositories.Account
{
    public interface IIdentityService
    {
     
        public   Task<ApplicationUser> RegisterAsync(DtoRegister register);
        public   Task<TokenResponse> Login(DtoLogin register);
    }
}
