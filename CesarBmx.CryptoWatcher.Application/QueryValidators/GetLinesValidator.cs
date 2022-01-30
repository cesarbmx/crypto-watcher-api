using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.CryptoWatcher.Domain.Types;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.QueryValidators
{
    public class GetLinesValidator : AbstractValidator<GetLines>
    {
        public GetLinesValidator()
        {
            RuleFor(x => x.Period)
                .NotNull()
                .WithMessage(nameof(IndicatorMessage.IndicatorIdHasInvalidFormat) + " " + IndicatorMessage.IndicatorIdHasInvalidFormat);

            RuleFor(x => x.Period)
                .Must(x=>x == Period.FIVE_MINUTES)
                .WithMessage(nameof(IndicatorMessage.IndicatorIdHasInvalidFormat) + " " + IndicatorMessage.IndicatorIdHasInvalidFormat);
        }
    }
}
