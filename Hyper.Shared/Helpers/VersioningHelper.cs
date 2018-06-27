using System;
using System.IO;
using System.Reflection;

namespace Hyper.Shared.Helpers
{
    public static class VersioningHelper
    {
        public static string VersionNumber(Assembly assembly)
        {
            var revisionNumbers = assembly.GetName().Version.ToString().Split('.');
            return revisionNumbers[0] + "." + revisionNumbers[1];
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