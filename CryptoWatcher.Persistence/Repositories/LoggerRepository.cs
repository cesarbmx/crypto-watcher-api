using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LoggerRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly Repository<TEntity> _repository;
        private readonly IRepository<Log> _logRepository;

        public LoggerRepository(Repository<TEntity> repository, IRepository<Log> logRepository)
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
        public async Task<List<TEntity>> GetAllNewest()
        {
            // Get newest
            return await _repository.GetAllNewest();
        }
        public async Task<TEntity> GetSingle(string id)
        {
            // Get by id
            return await _repository.GetSingle(id);
        }
        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
        {
            // Get single by expression
            return await _repository.GetSingle(expression);
        }
        public void Add(TEntity entity)
        {
            // Add
            _repository.Add(entity);

            // Log
            _logRepository.Add(new Log("Add", entity, entity.Id, entity.CreatedBy, entity.CreationTime));
        }
        public void AddRange(List<TEntity> entities)
        {
            // Return if no entities
            if (entities.Count == 0) return;

            // Add
            _repository.AddRange(entities);

            // Log
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }
        public void Update(TEntity entity)
        {
            // Log
            _logRepository.Add(new Log("Update", entity, entity.Id, entity.CreatedBy, entity.CreationTime));
        }
        public void UpdateRange(List<TEntity> entities)
        {
            // Log
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
            _logRepository.Add(new Log("Remove", entity, entity.Id, entity.CreatedBy, entity.CreationTime));
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
