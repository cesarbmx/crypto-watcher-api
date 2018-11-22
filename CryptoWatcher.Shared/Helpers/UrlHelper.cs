using System.Linq;


namespace CryptoWatcher.Shared.Helpers
{
    public static class UrlHelper
    {
        public static string BuildUrl(params string[]  args)
        {
            // Build Url
            var url = string.Empty;
            foreach (var arg in args)
            {
                url += "-" + arg.BuildFromCamelCaseToSnakeCase();
            }

            return url.Substring(1);
        }
        public static string BuildFromCamelCaseToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
        }
    }
}