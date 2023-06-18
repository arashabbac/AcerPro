using AcerPro.Application.Commands;
using AcerPro.Domain.ValueObjects;
using FluentValidation;

namespace AcerPro.Application.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Firstname)
            .NotEmpty()
                .WithMessage("First name is required");

        RuleFor(c => c.Lastname)
            .NotEmpty()
                .WithMessage("Last name is required");

        RuleFor(c => c.Email)
            .NotEmpty()
                .WithMessage("Email name is required")
            .Matches(Email.ValidEmailRegex)
                .WithMessage("Email is invalid");

    }
}
