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
                url += "-" + arg.BuildUrl();
            }

            return url.Substring(1);
        }
        public static string BuildUrl(this string str)
        {
            str = str.Replace(" ", "-");
            str = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
            str = str.Replace("--", "-");

            return str;
        }

    }
}