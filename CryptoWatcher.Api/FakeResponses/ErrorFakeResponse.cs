using System.Collections.Generic;
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
            var validationErrorResponseList = new List<ValidationErrorResponse>
            {
                new ValidationErrorResponse("Code", "FieldName", "Validation description")
            };
            var validationResponse = new ValidationResponse(nameof(Messages.ValidationFailed), 422, Messages.ValidationFailed, validationErrorResponseList);

            return validationResponse;
        }
        public static ErrorResponse GetFake_InternalServerError()
        {
            return new ErrorResponse(nameof(Messages.InternalServerError), 500, Messages.InternalServerError);
        }
    }
}
