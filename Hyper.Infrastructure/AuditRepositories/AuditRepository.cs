using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;
using System.Linq;

namespace Hyper.Persistence.AuditRepositories
{
    public class AuditRepository<T>: ICacheRepository where T: new()
    {
        private readonly List<T> _t;
        private readonly ILogRepository _logRepository;

        public AuditRepository(ILogRepository logRepository)
        {
            _t = new List<T>();
            _logRepository = logRepository;

            LoadAudit();
        }

        private void LoadAudit()
        {
            var log = _logRepository.GetLog().Result;
            foreach (var logEntry in log)
            {
                T originalValue;
                T newValue;
                switch (logEntry.ActionName)
                {
                    case "Add":
                        newValue = logEntry.ModelJsonToObject<T>();
                        _t.Add(newValue);
                        break;
                    case "Update":
                        newValue = logEntry.ModelJsonToObject<T>();
                        originalValue = GetByKey(originalValue.Key).Result;
                        originalValue = newValue;
                        break;
                    case "Delete":
                        newValue = logEntry.ModelJsonToObject<T>();
                        originalValue = GetByKey(originalValue.Key).Result;
                        _t.Remove(originalValue);
                        break;
                }
            }
        }

        public  Task<Cache> GetByKey(string key)
        {
            // Get cache
            return Task.FromResult(_cacheList.FirstOrDefault(CacheExpressions.HasKey(key).Compile()));
        }
        public void Add(Cache cache)
        {
            // Add
            _cacheList.Add(cache);
        }
    }
}
