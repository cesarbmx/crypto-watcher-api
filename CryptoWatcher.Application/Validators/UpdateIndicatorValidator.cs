using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class UpdateIndicatorValidator : AbstractValidator<UpdateIndicator>
    {
        public UpdateIndicatorValidator()
        {
            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.DependenciesMustBeProvided) + " " + IndicatorMessage.DependenciesMustBeProvided);
        }
    }
}
