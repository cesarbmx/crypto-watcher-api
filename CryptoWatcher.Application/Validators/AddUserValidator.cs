using CryptoWatcher.Application.Requests;
using CryptoWatcher.Domain.Messages;
using FluentValidation;

namespace CryptoWatcher.Application.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.UserId)
             .NotEmpty()
             .WithMessage(nameof(UserMessage.IdCannotBeEmpty) + " " + UserMessage.IdCannotBeEmpty);
        }
    }
}
