using CesarBmx.Notification.Application.Requests;
using CesarBmx.Notification.Application.Messages;
using FluentValidation;

namespace CesarBmx.Notification.Application.Validators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicator>
    {
        public AddIndicatorValidator()
        {
            RuleFor(x => x.Abbreviation)
                .Matches("^[A-Z0-9_]+$")
                .WithMessage(IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(IndicatorMessage.DependenciesMustBeProvided);
        }
    }
}
