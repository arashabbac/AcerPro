using AcerPro.Persistence.DTOs;
using AcerPro.Presentation.Server.Commands;
using FluentValidation;

namespace AcerPro.Presentation.Server.Validators;

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
