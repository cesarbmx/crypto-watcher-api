

using System.Collections.Generic;

namespace Hyper.Api.Responses
{
    public class ValidationResponse
    {
        public string Code { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ValidationErrorResponse> ValidationErrors { get; set; }

        public ValidationResponse(string code, int status, string message)
        {
            Code = code;
            Status = status;
            Message = message;
            ValidationErrors = new List<ValidationErrorResponse>();
        }
    }
}