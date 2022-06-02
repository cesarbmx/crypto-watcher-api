using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.RequestValidators
{
    public class AddUserValidator : AbstractValidator<AddUser>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.UserId)
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage(UserMessage.UserIdMustContainOnlyLettersOrNumbers);

            RuleFor(x => x.UserId)
                .Matches("^[a-z0-9]+$")
                .WithMessage(UserMessage.UserIdMustBeLowerCase);

            RuleFor(x => x.UserId)
                .Matches("^(?=[^A-Za-z]*[A-Za-z])[ -~]*$")
                .WithMessage(UserMessage.UserIdMustContainAtLeastOneLetter);
        }
    }
}
