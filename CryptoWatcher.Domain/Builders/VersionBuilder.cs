using System.Reflection;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Helpers;


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
