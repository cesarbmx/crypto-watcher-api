using Newtonsoft.Json.Converters;

namespace Hyper.Shared.Converters
{
    public class OnlyDateConverter: IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}