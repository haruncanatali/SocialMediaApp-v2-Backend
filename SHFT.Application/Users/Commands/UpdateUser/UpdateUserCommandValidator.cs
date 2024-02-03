using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
        }
    }
}
