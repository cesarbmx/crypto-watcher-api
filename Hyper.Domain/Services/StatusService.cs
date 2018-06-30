using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Shared.Helpers;
using Version = Hyper.Domain.Models.Version;

namespace Hyper.Domain.Services
{
    public class StatusService
    {
        private readonly Assembly _assembly;
        private readonly ICurrencyRepository _currencyRepository;

        public StatusService(
            Assembly assembly,
            ICurrencyRepository currencyRepository)
        {
            _assembly = assembly;
            _currencyRepository = currencyRepository;
        }

        public  Task<Version> GetVersion(string environment)
        {
            return Task.FromResult(
                new Version(
                    // VersionNumber
                    VersioningHelper.VersionNumber(_assembly),
                    // LastBuildOccurred
                    VersioningHelper.BuildDate(_assembly),
                    // Environment
                    environment
                ));
        }
        public async Task<Health> GetHealth()
        {
            var isEverythingOk = true;
            var isConnectionToDatabaseOk = true;
            var isResponseTimeAcceptable = true;

            // Check if connection to database is ok
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                await _currencyRepository.GetAllCurrencies();
                sw.Stop();
            }
            catch
            {
                isEverythingOk = false;
                isConnectionToDatabaseOk = false;
            }

            // Check if response time is ok
            if (sw.ElapsedMilliseconds > 5000)
            {
                isEverythingOk = false;
                isResponseTimeAcceptable = false;
            }

            // Return health
            var health = new Health(isEverythingOk, isConnectionToDatabaseOk, isResponseTimeAcceptable);
            return health;
        }
    }
}
