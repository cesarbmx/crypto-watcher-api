using System.Threading.Tasks;

namespace CryptoWatcher.Exchanges
{
    public interface IExchange
    {
        Task PlaceOrder(decimal quantity);
    }
}
