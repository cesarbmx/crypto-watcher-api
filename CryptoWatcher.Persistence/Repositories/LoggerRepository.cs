using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public async Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Get all
            return await _repository.GetAll(includeProperties);
        }
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Get all by expression
            return await _repository.GetAll(expression, includeProperties);
        }
        public async Task<TEntity> GetSingle(object id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Get by id
            return await _repository.GetSingle(id, includeProperties);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Get single by expression
            return await _repository.GetSingle(expression, includeProperties);
        }
        public void Add(TEntity entity)
        {
            // Add
            _repository.Add(entity);

            // Log
            _logRepository.Add(new Log("Add", entity, entity.Id));
        }
        public void AddRange(List<TEntity> entities)
        {
            // Add
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }
        public void Update(TEntity entity)
        {
            // Update
            _repository.Update(entity);

            // Log
            _logRepository.Add(new Log("Update", entity, entity.Id));
        }
        public void UpdateRange(List<TEntity> entities)
        {
            // Update
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        public void Remove(TEntity entity)
        {
            // Remove
            _repository.Remove(entity);

            // Log
            _logRepository.Add(new Log("Remove", entity, entity.Id));
        }
        public void RemoveRange(List<TEntity> entities)
        {
            // Remove
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }
    }
}
