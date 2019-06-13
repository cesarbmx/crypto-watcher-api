using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Builders;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LoggerRepository<TEntity>: IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly Repository<TEntity> _repository;
        private readonly Repository<Log> _logRepository;

        public LoggerRepository(Repository<TEntity> repository, Repository<Log> logRepository)
        {
            _repository = repository;
            _logRepository = logRepository;
        }

        public async Task<List<TEntity>> GetAll()
        {
            // Get all
            return await _repository.GetAll();
        }
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            // Get all by expression
            return await _repository.GetAll(expression);
        }
        public async Task<TEntity> GetSingle(object id)
        {
            // Get by id
            return await _repository.GetSingle(id);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            // Get single by expression
            return await _repository.GetSingle(expression);
        }
        public async Task<DateTime> GetNewestTime()
        {
            return await _repository.GetNewestTime();
        }
        public void Add(TEntity entity, DateTime time)
        {
            // Add
            _repository.Add(entity, time);

            // Log
            _logRepository.Add(new Log("Add", entity, entity.Id, time), time);
        }
        public void AddRange(List<TEntity> entities, DateTime time)
        {
            // Add
            foreach (var entity in entities)
            {
                Add(entity, time);
            }
        }
        public void Update(TEntity entity, DateTime time)
        {
            // Update
            _repository.Update(entity, time);

            // Log
            _logRepository.Add(new Log("Update", entity, entity.Id, time), time);
        }
        public void UpdateRange(List<TEntity> entities, DateTime time)
        {
            // Update
            foreach (var entity in entities)
            {
                Update(entity, time);
            }
        }
        public void Remove(TEntity entity, DateTime time)
        {
            // Remove
            _repository.Remove(entity, time);

            // Log
            _logRepository.Add(new Log("Remove", entity, entity.Id, time), time);
        }
        public void RemoveRange(List<TEntity> entities, DateTime time)
        {
            // Remove
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
