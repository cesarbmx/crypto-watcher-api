using System.Reflection;
using CesarBmx.Shared.Common.Extensions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.ModelBuilders
{
    public static class VersionBuilder
    {
        public static Version BuildVersion(Assembly assembly)
        {
            return new Version(
                // VersionNumber
                assembly.VersionNumber(),
                // LastBuildOccurred
                assembly.Date()
             );
        }
    }
}
