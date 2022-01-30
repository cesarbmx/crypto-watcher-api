using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.Shared.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.QueryValidators
{
    public class GetLinesValidator : AbstractValidator<GetLines>
    {
        public GetLinesValidator()
        {
            RuleFor(x => x.Period)
                .NotNull()
                .WithMessage(nameof(ErrorMessage.Required) + " " + ErrorMessage.Required);
        }
    }
}
