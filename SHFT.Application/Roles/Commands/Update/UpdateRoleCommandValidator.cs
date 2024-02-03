using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Roles.Commands.Update;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.RoleName);
        RuleFor(c => c.Id).NotNull()
            .WithName(GlobalPropertyDisplayName.RoleId);
    }
}