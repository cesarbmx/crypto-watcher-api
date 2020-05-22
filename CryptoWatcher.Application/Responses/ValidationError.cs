

namespace CryptoWatcher.Application.Responses
{
    public class ValidationError
    {
        public string Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }

        public ValidationError(string code, string field, string message)
        {
            Code = code;
            Field = field;
            Message = message;
        }
    }
}