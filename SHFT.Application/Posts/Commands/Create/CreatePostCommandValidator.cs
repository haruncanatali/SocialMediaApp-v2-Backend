using FluentValidation;
using SHFT.Application.Common.Models;

namespace SHFT.Application.Posts.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
            .WithName(GlobalPropertyDisplayName.Content);
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithName(GlobalPropertyDisplayName.Title);
    }
}