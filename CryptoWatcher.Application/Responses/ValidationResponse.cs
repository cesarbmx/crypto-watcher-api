

using System.Collections.Generic;

namespace CryptoWatcher.Application.Responses
{
    public class ValidationResponse
    {
        public string Code { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }

        public ValidationResponse(string code, int status, string message, List<ValidationError> validationErrors)
        {
            Code = code;
            Status = status;
            Message = message;
            ValidationErrors = validationErrors;
        }
    }
}