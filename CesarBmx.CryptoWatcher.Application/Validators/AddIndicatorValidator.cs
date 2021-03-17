using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.Validators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicator>
    {
        public AddIndicatorValidator()
        {
            RuleFor(x => x.IndicatorId)
                .Matches("^[a-z\\d-]+$")
                .WithMessage(nameof(IndicatorMessage.IndicatorIdHasInvalidFormat) + " " + IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DependenciesMustBeProvided) + " " + IndicatorMessage.DependenciesMustBeProvided);
        }
    }
}
