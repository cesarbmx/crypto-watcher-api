using System.Threading.Tasks;

namespace CryptoWatcher.Exchanges
{
    public class FakeExchange: IExchange
    {
        public Task PlaceOrder(decimal quantity)
        {
            return Task.CompletedTask;
        }
    }
}
