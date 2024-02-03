using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Posts.Commands.Update;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
            .WithName(GlobalPropertyDisplayName.Content);
        RuleFor(c => c.Id)
            .NotNull()
            .WithName(GlobalPropertyDisplayName.PostId);
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithName(GlobalPropertyDisplayName.Title);
    }
}