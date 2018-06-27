using Hyper.Api.Responses;
using Hyper.Domain.Messages;
using Hyper.Shared.Extensions;

namespace Hyper.Api.FakeResponses
{
    public static class ErrorFakeResponse
    {
        public static ErrorResponse GetFake_BadRequest()
        {
            return new ErrorResponse(ServiceMessages.InvalidRequest.GetCode(), 400, ServiceMessages.InvalidRequest.GetMessage());
        }
        public static ErrorResponse GetFake_NotFound()
        {
            return new ErrorResponse(ServiceMessages.ResourceNotFound.GetCode(), 404, ServiceMessages.ResourceNotFound.GetMessage());
        }
        public static ErrorResponse GetFake_Conflict()
        {
            return new ErrorResponse(ServiceMessages.Conflict.GetCode(), 409, ServiceMessages.Conflict.GetMessage());
        }
        public static ValidationResponse GetFake_InvalidRequest()
        {
            var validationResponse = new ValidationResponse(ServiceMessages.ValidationFailed.GetCode(), 422, ServiceMessages.ValidationFailed.GetMessage());
            validationResponse.ValidationErrors.Add(
                new ValidationErrorResponse("#0000", "FieldName", "Validation description")
            );
            return validationResponse;
        }
        public static ErrorResponse GetFake_InternalServerError()
        {
            return new ErrorResponse(ServiceMessages.InternalServerError.GetCode(), 500, ServiceMessages.InternalServerError.GetMessage());
        }
    }
}
