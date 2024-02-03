using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Auth.Queries.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithName(GlobalPropertyDisplayName.Password);
            RuleFor(x => x.UserName).NotEmpty().WithName(GlobalPropertyDisplayName.UserName);
        }
    }
}
