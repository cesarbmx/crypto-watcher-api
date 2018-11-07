using System.Collections.Generic;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Hyper.Persistence.AuditRepositories
{
    public abstract class AuditRepository<TEntity> : IRepository<TEntity> where TEntity: Entity
    {
        protected readonly List<TEntity> List;
        private readonly ILogRepository _logRepository;

        public AuditRepository(ILogRepository logRepository)
        {
            List = new List<TEntity>();
            _logRepository = logRepository;

            LoadAudit();
        }

        private void LoadAudit()
        {
            var log = _logRepository.GetAll().Result;

            foreach (var logEntry in log)
            {
                TEntity originalValue;
                TEntity newValue;
                switch (logEntry.Action)
                {
                    case "Add":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        List.Add(newValue);
                        break;
                    case "Update":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        originalValue = GetByKey(newValue.Id).Result;
                        var index = List.IndexOf(originalValue);
                        if (index != -1) List[index] = newValue;
                        break;
                    case "Remove":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        originalValue = GetByKey(newValue.Id).Result;
                        List.Remove(originalValue);
                        break;
                }
            }
        }

        public Task<List<TEntity>> GetAll()
        {
            return Task.FromResult(List);
        }
        public Task<TEntity> GetByKey(string id)
        {
            return Task.FromResult(List.FirstOrDefault(x=>x.Id == id));
        }
        public void Add(TEntity entity)
        {
            List.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            List.Remove(entity);
        }
    }
}
