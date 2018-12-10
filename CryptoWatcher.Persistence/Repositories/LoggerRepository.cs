using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
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

            // Add log
            _logRepository.Add(new Log("Add", entity,  entity.Id, entity.CreatedBy));
        }
        public void AddRange(List<TEntity> entities)
        {
            // Return if no entities
            if (entities.Count == 0) return;

            // Add range
            _repository.AddRange(entities);

            foreach (var entity in entities)
            {
                // Add log
                _logRepository.Add(new Log("Add", entities, entity.Id, entity.CreatedBy));
            }
        }
        public void Update(TEntity entity)
        {
            // Update
            _repository.Update(entity);

            // Add log
            _logRepository.Add(new Log("Update", entity,  entity.Id, entity.CreatedBy));
        }
        public void Remove(TEntity entity)
        {
            // Remove
            _repository.Remove(entity);

            // Add log
            _logRepository.Add(new Log("Remove", entity, entity.Id, entity.CreatedBy));
        }
    }
}
