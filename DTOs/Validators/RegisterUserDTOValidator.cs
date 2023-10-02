using FluentValidation;
using ProjektDaniel.Data;

namespace ProjektDaniel.DTOs.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(UżytkownikDbContext dbContext) { 

        RuleFor(x=>x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Password).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(e =>e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Użytkownicy.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
