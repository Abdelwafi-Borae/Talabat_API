using Microsoft.AspNetCore.Identity;
using Data.Model;
using Data.ModelViews;
using Data.DbContext;

namespace Data.Repositories.Account
{
    public class Register : IServiceAccount<DtoRegister>
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public Register(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<ModelError> Add(DtoRegister register)
        {
            var appUser = await _userManager.FindByEmailAsync(register.Email);
            if (appUser == null) // user not found register it
            {
                ApplicationUser applicationUser = new()
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    Address = register.Address,
                    PhoneNumber = register.PhoneNumber,
                    Role = register.Role.ToString(),
                    Name = register.Name
                };
                IdentityResult result = await _userManager.CreateAsync(applicationUser, register.Password);
                if (result.Succeeded)
                {
                    // assign this user to role
                    IdentityResult resultRole =  await _userManager.AddToRoleAsync(applicationUser, register.Role.ToString());
                    if (resultRole.Succeeded)
                    {
                        return new ModelError() { IsError = false };
                    }
                    else
                        return new ModelError() { IsError = true, Message = "Faild to assign role for this user !", identityErrors = resultRole.Errors };
                }
                else
                    return new ModelError() { IsError = true, Message = "Faild to register !", identityErrors = result.Errors };
            }
            return new ModelError(){ IsError = true, Message = "this email is already exist !" };
        }
    }
}
