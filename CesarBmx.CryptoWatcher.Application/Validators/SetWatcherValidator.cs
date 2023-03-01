using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.Validators
{
    public class SetWatcherValidator : AbstractValidator<SetWatcher>
    {
        public SetWatcherValidator()
        {
            RuleFor(x => x.Buy)
                .Must(x => x > 0)
                .WithMessage(WatcherMessage.LimitOrderMustBeHigherThanZero);

            RuleFor(x => x.Sell)
                .Must(x => x > 0)
                .WithMessage(WatcherMessage.LimitOrderMustBeHigherThanZero);

            RuleFor(x => x.Sell)
                .Must((x, sell) => !sell.HasValue || sell > x.Buy)
                .WithMessage(WatcherMessage.SellLimitMustBeHigherThanBuyLimit);
        }
    }
}
