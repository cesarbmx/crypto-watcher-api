using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.Shared.Application.Messages;
using FluentValidation;

namespace CesarBmx.CryptoWatcher.Application.QueryValidators
{
    public class GetScriptVariablesValidator : AbstractValidator<GetScriptVariables>
    {
        public GetScriptVariablesValidator()
        {
            RuleFor(x => x.Period)
                .NotNull()
                .WithMessage(nameof(ErrorMessage.Required) + " " + ErrorMessage.Required);
        }
    }
}
