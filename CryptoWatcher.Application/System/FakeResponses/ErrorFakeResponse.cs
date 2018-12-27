using System.Collections.Generic;
using CryptoWatcher.Application.System.Responses;
using CryptoWatcher.Domain.Messages;

namespace CryptoWatcher.Application.System.FakeResponses
{
    public static class ErrorFakeResponse
    {
        public static ErrorResponse GetFake_BadRequest()
        {
          
            return new ErrorResponse(nameof(Message.BadRequest), 400, Message.BadRequest);
        }
        public static ErrorResponse GetFake_NotFound()
        {
            return new ErrorResponse(nameof(Message.NotFound), 404, Message.NotFound);
        }
        public static ErrorResponse GetFake_Conflict()
        {
            return new ErrorResponse(nameof(Message.Conflict), 409, Message.Conflict);
        }
        public static ValidationResponse GetFake_ValidationFailed()
        {
            var validationErrors = new List<ValidationError>
            {
                new ValidationError("Code", "FieldName", "Validation description")
            };
            var validationResponse = new ValidationResponse(nameof(Message.ValidationFailed), 422, Message.ValidationFailed, validationErrors);

            return validationResponse;
        }
        public static ErrorResponse GetFake_InternalServerError()
        {
            return new ErrorResponse(nameof(Message.InternalServerError), 500, Message.InternalServerError);
        }
    }
}
