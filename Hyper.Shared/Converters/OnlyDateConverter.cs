using Newtonsoft.Json.Converters;

namespace CryptoWatcher.Shared.Converters
{
    public class OnlyDateConverter: IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}