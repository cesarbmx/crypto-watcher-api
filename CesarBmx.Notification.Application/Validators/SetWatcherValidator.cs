using CesarBmx.Notification.Application.Requests;
using CesarBmx.Notification.Application.Messages;
using FluentValidation;

namespace CesarBmx.Notification.Application.Validators
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
