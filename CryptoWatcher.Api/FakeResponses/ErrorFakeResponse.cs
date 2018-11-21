using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class ErrorFakeResponse
    {
        public static ErrorResponse GetFake_BadRequest()
        {
          
            return new ErrorResponse(nameof(Messages.InvalidRequest), 400, Messages.InvalidRequest);
        }
        public static ErrorResponse GetFake_NotFound()
        {
            return new ErrorResponse(nameof(Messages.NotFound), 404, Messages.NotFound);
        }
        public static ErrorResponse GetFake_Conflict()
        {
            return new ErrorResponse(nameof(Messages.Conflict), 409, Messages.Conflict);
        }
        public static ValidationResponse GetFake_InvalidRequest()
        {
            var validationResponse = new ValidationResponse(nameof(Messages.ValidationFailed), 422, Messages.ValidationFailed);
            validationResponse.ValidationErrors.Add(
                new ValidationErrorResponse("#0000", "FieldName", "Validation description")
            );
            return validationResponse;
        }
        public static ErrorResponse GetFake_InternalServerError()
        {
            return new ErrorResponse(nameof(Messages.InternalServerError), 500, Messages.InternalServerError);
        }
    }
}
