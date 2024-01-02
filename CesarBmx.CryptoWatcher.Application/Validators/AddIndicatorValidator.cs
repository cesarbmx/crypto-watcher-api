using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.Validators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicatorRequest>
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
