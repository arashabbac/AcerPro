using AcerPro.Application.Commands;
using FluentValidation;

namespace AcerPro.Application.Validators;

public class AddTargetAppCommandValidator : AbstractValidator<AddTargetAppCommand>
{
    public AddTargetAppCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage("Name is required");

        RuleFor(c => c.UrlAddress)
            .NotEmpty()
                .WithMessage("UrlAddress is required");

        RuleFor(c => c.MonitoringIntervalInSeconds)
            .NotEmpty()
                .WithMessage("MonitoringIntervalInSeconds is required")
            .GreaterThan(0)
                .WithMessage("MonitoringIntervalInSeconds must be greater than 0");
    }
}
