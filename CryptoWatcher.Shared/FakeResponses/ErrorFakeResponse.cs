using System.Collections.Generic;
using CryptoWatcher.Shared.Responses;
using CryptoWatcher.Shared.Messages;

namespace CryptoWatcher.Shared.FakeResponses
{
    public static class ErrorFakeResponse
    {
        public static ErrorResponse GetFake_BadRequest()
        {
          
            return new ErrorResponse(nameof(Message.InvalidRequest), 400, Message.InvalidRequest);
        }
        public static ErrorResponse GetFake_NotFound()
        {
            return new ErrorResponse(nameof(Message.NotFound), 404, Message.NotFound);
        }
        public static ErrorResponse GetFake_Conflict()
        {
            return new ErrorResponse(nameof(Message.Conflict), 409, Message.Conflict);
        }
        public static ValidationResponse GetFake_InvalidRequest()
        {
            var validationErrorResponseList = new List<ValidationErrorResponse>
            {
                new ValidationErrorResponse("Code", "FieldName", "Validation description")
            };
            var validationResponse = new ValidationResponse(nameof(Message.ValidationFailed), 422, Message.ValidationFailed, validationErrorResponseList);

            return validationResponse;
        }
        public static ErrorResponse GetFake_InternalServerError()
        {
            return new ErrorResponse(nameof(Message.InternalServerError), 500, Message.InternalServerError);
        }
    }
}
