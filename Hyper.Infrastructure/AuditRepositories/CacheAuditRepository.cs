using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;
using System.Linq;

namespace Hyper.Persistence.AuditRepositories
{
    public class CacheAuditRepository: ICacheRepository
    {
        private readonly List<Cache> _cacheList;
        private readonly ILogRepository _logRepository;

        public CacheAuditRepository(ILogRepository logRepository)
        {
            _cacheList = new List<Cache>();
            _logRepository = logRepository;

            LoadAudit();
        }

        private void LoadAudit()
        {
            var log = _logRepository.GetLog().Result;
            foreach (var logEntry in log)
            {
                Cache originalValue;
                Cache newValue;
                switch (logEntry.ActionName)
                {
                    case "Add":
                        newValue = logEntry.ModelJsonToObject<Cache>();
                        _cacheList.Add(newValue);
                        break;
                    case "Update":
                        newValue = logEntry.ModelJsonToObject<Cache>();
                        originalValue = GetByKey(newValue.Key).Result;
                        var index = _cacheList.IndexOf(originalValue);
                        if (index != -1)  _cacheList[index] = newValue;
                        break;
                    case "Delete":
                        newValue = logEntry.ModelJsonToObject<Cache>();
                        originalValue = GetByKey(newValue.Key).Result;
                        _cacheList.Remove(originalValue);
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
            // Not supported
            throw new NotSupportedException();
        }
    }
}
