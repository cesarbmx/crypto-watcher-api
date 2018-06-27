using System;

namespace Hyper.Shared.Extensions
{
    public static class UrlExtensions
    {
        public static bool IsUrl(this string input)
        {
            Uri uriResult;
            return Uri.TryCreate(input, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
