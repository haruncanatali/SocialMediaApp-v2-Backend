using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.FallowedPosts.Commands.Create;

public class CreateFallowedPostCommandValidator : AbstractValidator<CreateFallowedPostCommand>
{
    public CreateFallowedPostCommandValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
            .WithName(GlobalPropertyDisplayName.PostId);
    }
}