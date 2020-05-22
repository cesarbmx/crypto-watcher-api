using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class AddWatcherValidator : AbstractValidator<AddWatcher>
    {
        public AddWatcherValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(nameof(UserMessage.UserIdCannotBeEmpty) + " " + UserMessage.UserIdCannotBeEmpty);
        }
    }
}
