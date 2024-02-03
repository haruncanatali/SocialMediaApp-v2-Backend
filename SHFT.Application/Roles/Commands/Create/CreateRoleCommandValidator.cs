using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Roles.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithName(GlobalPropertyDisplayName.RoleName);
    }
}