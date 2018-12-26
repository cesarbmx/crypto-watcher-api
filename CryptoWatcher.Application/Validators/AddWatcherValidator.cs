using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class AddWatcherValidator : AbstractValidator<AddWatcherRequest>
    {
        public AddWatcherValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(nameof(UserMessage.UserIdCannotBeEmpty) + " " + UserMessage.UserIdCannotBeEmpty);
        }
    }
}
