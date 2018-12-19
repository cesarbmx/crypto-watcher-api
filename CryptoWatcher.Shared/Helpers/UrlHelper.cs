using System.Linq;


namespace CryptoWatcher.Shared.Helpers
{
    public static class UrlHelper
    {
        public static string BuildSeoFriendlyUrl(params string[]  args)
        {
            // Build Url
            var url = string.Empty;
            foreach (var arg in args)
            {
                url += "-" + arg.BuildSeoFriendlyUrl();
            }

            return url.Substring(1);
        }
        public static string BuildSeoFriendlyUrl(this string str)
        {
            str = str.Replace(" ", "-");
            str = string.Concat(str.Select((x, i) => i > 0 && (char.IsUpper(x) || char.IsDigit(x)) ? "-" + x.ToString() : x.ToString())).ToLower();
            str = str.Replace("--", "-");

            return str;
        }

    }
}