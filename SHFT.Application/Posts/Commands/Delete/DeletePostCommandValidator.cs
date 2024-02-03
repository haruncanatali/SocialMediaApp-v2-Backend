using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Posts.Commands.Delete;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .WithName(GlobalPropertyDisplayName.PostId);
    }
}