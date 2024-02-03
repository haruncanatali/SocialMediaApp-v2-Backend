using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Auth.Queries.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(c => c.RefreshToken)
            .NotNull()
            .WithName(GlobalPropertyDisplayName.RefreshToken);
    }
}