using Microsoft.AspNetCore.Identity;
using Data.Model;
using Data.ModelViews;
using Data.DbContext;
using Data.Common.Exceptions;
using Data.Validators;
using Data.Common.Extensions;
using ValidationException = Data.Common.Exceptions.ValidationException;
using Tawtheiq.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Win32;
using FluentValidation;
using Azure.Core;


namespace Data.Repositories.Account
{
    public class IdentityService : IIdentityService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public IdentityService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        //public async Task<ModelError> Add(DtoRegister register)
        public async Task<ApplicationUser> RegisterAsync(DtoRegister register)
        {

            var validator = new RegisterVAlidato().Validate(register);
            if (!validator.IsValid) throw new ValidationException(validator.Errors);
            var appUser = await _userManager.FindByEmailAsync(register.Email);
            _ = appUser ?? throw new ForbiddenAccessException("Invalid Credentials");

            ApplicationUser applicationUser = new()
            {
                UserName = register.UserName,
                Email = register.Email,
                Address = register.Address,
                PhoneNumber = register.PhoneNumber,
                Role = register.Role.ToString(),
                Name = register.Name
            };
          
            var existingUser = await GetExistingUser(applicationUser);
            if (existingUser is not null)
            {
                throw new AlreadyExistException("Email", "Email already exist");

            }
            IdentityResult result = await _userManager.CreateAsync(applicationUser, register.Password);
            if (!result.Succeeded) throw result.ToValidationException();

            // assign this user to role
            IdentityResult resultRole = await _userManager.AddToRoleAsync(applicationUser, register.Role.ToString());
            if (!resultRole.Succeeded) throw result.ToValidationException();

            return applicationUser;



        }
        public async Task<TokenResponse> Login(DtoLogin model)
        {
            var validator = new LoginVAlidator().Validate(model);
            if (!validator.IsValid) throw new ValidationException(validator.Errors);
           
            var user = await _userManager.FindByEmailAsync(model.Email.Trim().Normalize());
            _ = user ?? throw new ForbiddenAccessException("Invalid Credentials");


            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                throw new ForbiddenAccessException("Invalid Credentials");


            var tokenString = GenerateJWTToken(model.Email, user.Role);

                 

                    return await Task.FromResult(new TokenResponse (tokenString,  DateTime.UtcNow.AddHours(1), user.Id ));
                }
            
        
        private string GenerateJWTToken(string Email, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:TokenKey").Value!);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, Email));
            claims.Add(new Claim(ClaimTypes.Role, Role));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration.GetSection("JWT:Issuer").Value,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        private async Task<ApplicationUser> GetExistingUser(ApplicationUser user)
        {
            var exitingUser = await _userManager.FindByEmailAsync(user.Email.Normalize());


            return exitingUser;
        }
    }
}

