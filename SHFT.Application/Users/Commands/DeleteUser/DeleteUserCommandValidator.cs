using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithName(GlobalPropertyDisplayName.UserId);
        }
    }
}
