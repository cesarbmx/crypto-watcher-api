using CesarBmx.Notification.Application.Requests;
using CesarBmx.Notification.Application.Messages;
using FluentValidation;

namespace CesarBmx.Notification.Application.Validators
{
    public class UpdateIndicatorValidator : AbstractValidator<UpdateIndicator>
    {
        public UpdateIndicatorValidator()
        {
            RuleFor(x => x.Dependencies)
                .NotNull()
                .WithMessage(IndicatorMessage.DependenciesMustBeProvided);
        }
    }
}
