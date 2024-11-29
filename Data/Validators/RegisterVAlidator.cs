using Data.ModelViews;
using FluentValidation;

namespace Data.Validators
{
    public class RegisterVAlidato : AbstractValidator<DtoRegister>
    {
        public RegisterVAlidato()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                    .WithMessage("Invalid Email Address.");

            RuleFor(p => p.Password)
                .NotEmpty().MinimumLength(6).MaximumLength(12);
            RuleFor(p => p.Role).NotEmpty();
        }
    } public class LoginVAlidator : AbstractValidator<DtoLogin>
    {
        public LoginVAlidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress()
                    .WithMessage("Invalid Email Address.");

            RuleFor(p => p.Password)
                .NotEmpty().MinimumLength(6).MaximumLength(12);
            
        }
    }
}
    

