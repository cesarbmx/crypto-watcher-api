using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using System.Linq;

namespace Hyper.Persistence.AuditRepositories
{
    public abstract class AuditRepository<T> where T:IEntity
    {
        protected readonly List<T> List;
        private readonly ILogRepository _logRepository;

        public AuditRepository(ILogRepository logRepository)
        {
            List = new List<T>();
            _logRepository = logRepository;

            LoadAudit();
        }

        private void LoadAudit()
        {
            var log = _logRepository.GetAll().Result;
            foreach (var logEntry in log)
            {
                T originalValue;
                T newValue;
                switch (logEntry.ActionName)
                {
                    case "Add":
                        newValue = logEntry.ModelJsonToObject<T>();
                        List.Add(newValue);
                        break;
                    case "Update":
                        newValue = logEntry.ModelJsonToObject<T>();
                        originalValue = GetById(newValue.Id).Result;
                        var index = List.IndexOf(originalValue);
                        if (index != -1) List[index] = newValue;
                        break;
                    case "Delete":
                        newValue = logEntry.ModelJsonToObject<T>();
                        originalValue = GetById(newValue.Id).Result;
                        List.Remove(originalValue);
                        break;
                }
            }
        }

        public Task<List<T>> GetAll()
        {
            return Task.FromResult(List);
        }
        public Task<T> GetById(int id)
        {
            // Get t
            return Task.FromResult(List.FirstOrDefault(x=>x.Id == id));
        }
        public void Add(T t)
        {
            // Not supported
            throw new NotSupportedException();
        }


    }
}
