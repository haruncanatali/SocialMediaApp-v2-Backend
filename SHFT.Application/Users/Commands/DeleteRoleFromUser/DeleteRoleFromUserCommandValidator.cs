using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Users.Commands.DeleteRoleFromUser;

public class DeleteRoleFromUserCommandValidator : AbstractValidator<DeleteRoleFromUserCommand>
{
    public DeleteRoleFromUserCommandValidator()
    {
        RuleFor(c => c.RoleId).NotNull()
            .WithName(GlobalPropertyDisplayName.RoleId);
        RuleFor(c => c.UserId).NotNull()
            .WithName(GlobalPropertyDisplayName.UserId);
    }
}