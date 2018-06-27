

namespace Hyper.Api.Responses
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }

        public ErrorResponse(string code, int status, string message)
        {
            Code = code;
            Status = status;
            Message = message;
        }
    }
}