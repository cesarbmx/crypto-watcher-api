using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class UpdateIndicatorValidator : AbstractValidator<UpdateIndicatorRequest>
    {
        public UpdateIndicatorValidator()
        {
            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DepenedenciesMustBeProvided) + " " + IndicatorMessage.DepenedenciesMustBeProvided);
        }
    }
}
