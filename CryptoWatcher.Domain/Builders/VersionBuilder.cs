using System.Reflection;
using CryptoWatcher.Shared.Helpers;
using CryptoWatcher.Shared.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class VersionBuilder
    {
        public static Version BuildVersion(Assembly assembly)
        {
            return new Version(
                 // VersionNumber
                 VersioningHelper.VersionNumber(assembly),
                 // LastBuildOccurred
                 VersioningHelper.BuildDate(assembly)
             );
        }
    }
}
