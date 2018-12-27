using MediatR;


namespace CryptoWatcher.Application.Lines.Requests
{
    public class RemoveOldLinesRequest : IRequest
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
