

namespace Hyper.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string GetCode(this string str)
        {
            return str.Substring(1, 5);
        }
        public static string GetMessage(this string str)
        {
            return str.Substring(8);
        }
    }
}
