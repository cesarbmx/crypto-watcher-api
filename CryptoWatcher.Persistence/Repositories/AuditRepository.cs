using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Shared.Providers;

namespace CryptoWatcher.Persistence.Repositories
{
    public class AuditRepository<TEntity> : IRepository<TEntity> where TEntity: Entity
    {
        protected readonly List<TEntity> List;
        private readonly IRepository<Log> _logRepository;

        public AuditRepository(IRepository<Log> logRepository, IDateTimeProvider dateTimeProvider)
        {
            List = new List<TEntity>();
            _logRepository = logRepository;

            LoadAudit(dateTimeProvider.GetDateFromHeader());
        }

        private void LoadAudit(DateTime dateTime)
        {
            var log = _logRepository.Get(LogExpression.AuditLog(dateTime)).Result;

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
                    case "AddRange":
                        var newValues = logEntry.ModelJsonToObject<List<TEntity>>();
                        List.AddRange(newValues);
                        break;
                    case "Update":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        originalValue = GetById(newValue.Id).Result;
                        var index = List.IndexOf(originalValue);
                        if (index != -1) List[index] = newValue;
                        break;
                    case "Remove":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        originalValue = GetById(newValue.Id).Result;
                        List.Remove(originalValue);
                        break;
                }
            }
        }

        public Task<List<TEntity>> GetAll()
        {
            return Task.FromResult(List);
        }

        public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(List.Where(expression.Compile()).ToList());
        }
        public Task<TEntity> GetById(string id)
        {
            return Task.FromResult(List.FirstOrDefault(x=>x.Id == id));
        }
        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(List.FirstOrDefault(expression.Compile()));
        }
        public void Add(TEntity entity)
        {
            List.Add(entity);
        }
        public void AddRange(List<TEntity> entities)
        {
            List.AddRange(entities);
        }
        public void Update(TEntity entity)
        {
        }
        public void Remove(TEntity entity)
        {
            List.Remove(entity);
        }
    }
}
