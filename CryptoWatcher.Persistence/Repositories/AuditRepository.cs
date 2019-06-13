using System;
using System.Collections.Generic;
using CryptoWatcher.Domain.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Shared.Providers;

namespace CryptoWatcher.Persistence.Repositories
{
    public class AuditRepository<TEntity>: IRepository<TEntity> where TEntity: class, IEntity
    {
        protected readonly List<TEntity> List;
        private readonly Repository<Log> _logRepository;

        public AuditRepository(Repository<Log> logRepository, DateTimeProvider dateTimeProvider)
        {
            List = new List<TEntity>();
            _logRepository = logRepository;

            LoadAudit(dateTimeProvider.GetDateTime());
        }

        private void LoadAudit(DateTime dateTime)
        {
            var log = _logRepository.GetAll(LogExpression.AuditLog(typeof(TEntity).Name, dateTime)).Result;

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

        public Task<List<TEntity>> GetAll()
        {
            return Task.FromResult(List);
        }
        public Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(List.Where(expression.Compile()).ToList());
        }
        public Task<TEntity> GetSingle(object id)
        {
            return Task.FromResult(List.FirstOrDefault(x=>x.Id == (string)id));
        }
        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            return Task.FromResult(List.FirstOrDefault(expression.Compile()));
        }
        public Task<DateTime> GetNewestTime()
        {
            return Task.FromResult(List.Max(x => x.Time));
        }
        public void Add(TEntity entity, DateTime time)
        {
            List.Add(entity);
        }
        public void AddRange(List<TEntity> entities, DateTime time)
        {
            List.AddRange(entities);
        }
        public void Update(TEntity entity, DateTime time)
        {

        }
        public void UpdateRange(List<TEntity> entities, DateTime time)
        {
            
        }
        public void Remove(TEntity entity, DateTime time)
        {
            List.Remove(entity);
        }
        public void RemoveRange(List<TEntity> entities, DateTime time)
        {
            foreach (var entity in entities)
            {
                Remove(entity, time);
            }
        }
        public void UpdateCollection(List<TEntity> currentEntities, List<TEntity> newEntities, DateTime time)
        {
            AddRange(EntityBuilder.BuildEntitiesToAdd(currentEntities, newEntities), time);
            UpdateRange(EntityBuilder.BuildEntitiesToUpdate(currentEntities, newEntities), time);
            RemoveRange(EntityBuilder.BuildEntitiesToRemove(currentEntities, newEntities), time);
        }
    }
}
