using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.FallowedPosts.Commands.Delete;

public class DeleteFallowedPostCommandValidator : AbstractValidator<DeleteFallowedPostCommand>
{
    public DeleteFallowedPostCommandValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
            .WithName(GlobalPropertyDisplayName.PostId);
    }
}