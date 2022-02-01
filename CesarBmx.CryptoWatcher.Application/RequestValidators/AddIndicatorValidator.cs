using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.RequestValidators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicator>
    {
        public AddIndicatorValidator()
        {
            RuleFor(x => x.Abbreviation)
                .Matches("^[A-z\\d-]+$")
                .WithMessage(IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(IndicatorMessage.DependenciesMustBeProvided);
        }
    }
}
