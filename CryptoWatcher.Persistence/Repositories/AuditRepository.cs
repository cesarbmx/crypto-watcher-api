using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Shared.Providers;

namespace CryptoWatcher.Persistence.Repositories
{
    public class AuditRepository<TEntity>: IRepository<TEntity> where TEntity: IEntity
    {
        protected readonly List<TEntity> List;
        private readonly Repository<Log> _logRepository;

        public AuditRepository(Repository<Log> logRepository, DateTimeProvider dateTimeProvider)
        {
            List = new List<TEntity>();
            _logRepository = logRepository;

            LoadAudit(dateTimeProvider.GetDateFromHeader());
        }

        private void LoadAudit(DateTime dateTime)
        {
            var log = _logRepository.GetAll(LogExpression.AuditLog(dateTime)).Result;

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
                        originalValue = GetSingle(newValue.Id).Result;
                        var index = List.IndexOf(originalValue);
                        if (index != -1) List[index] = newValue;
                        break;
                    case "Remove":
                        newValue = logEntry.ModelJsonToObject<TEntity>();
                        originalValue = GetSingle(newValue.Id).Result;
                        List.Remove(originalValue);
                        break;
                }
            }
        }

        public Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Task.FromResult(List);
        }
        public Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Task.FromResult(List.Where(expression.Compile()).ToList());
        }
        public Task<TEntity> GetSingle(object id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Task.FromResult(List.FirstOrDefault(x=>x.Id == (string)id));
        }
        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
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
        public void UpdateRange(List<TEntity> entities)
        {
            
        }
        public void Remove(TEntity entity)
        {
            List.Remove(entity);
        }
        public void RemoveRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }
    }
}
