using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class AddIndicatorValidator : AbstractValidator<AddIndicator>
    {
        public AddIndicatorValidator()
        {
            RuleFor(x => x.IndicatorId)
                .Matches("^[A-Z\\d-]+$")
                .WithMessage(nameof(IndicatorMessage.IndicatorIdHasInvalidFormat) + " " + IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DepenedenciesMustBeProvided) + " " + IndicatorMessage.DepenedenciesMustBeProvided);
        }
    }
}
