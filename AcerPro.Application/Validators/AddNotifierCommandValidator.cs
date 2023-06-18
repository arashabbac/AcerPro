using AcerPro.Application.Commands;
using AcerPro.Persistence.DTOs;
using FluentValidation;

namespace AcerPro.Application.Validators;

public class AddNotifierCommandValidator : AbstractValidator<AddNotifierCommand>
{
    public AddNotifierCommandValidator()
    {
        RuleFor(c => c.Address)
            .NotEmpty()
                .WithMessage("Address is required");

        RuleFor(c => (NotifierTypeDto)c.NotifierType)
            .IsInEnum()
                .WithMessage("NotifierType has incorrect value")
            ;
    }
}
