using System;
using System.IO;
using System.Reflection;

namespace CryptoWatcher.Shared.Helpers
{
    public static class VersioningHelper
    {
        public static string VersionNumber(Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }
        public static DateTime BuildDate(Assembly assembly)
        {
            // Build date
            var location = assembly.Location;
            if (location == null) throw new NotImplementedException("Asembly.Location is null");
            var builDate = File.GetLastWriteTime(location);
            return builDate;
        }
    }
}