using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Domain.Builders
{
    public static class EntityBuilder
    {
        public static List<T> BuildEntitiesToAdd<T>(List<T> entities, List<T> newEntities) where T : class, IEntity
        {
            // Add those not found in the list
            var entitiesToAdd = new List<T>();
            foreach (var newEntity in newEntities)
            {
                var entityExpression = EntityExpression.Entity(newEntity.Id);
                if (entities.FirstOrDefault(entityExpression.Compile()) == null)
                    entitiesToAdd.Add(newEntity);
            }

            // Return
            return entitiesToAdd;
        }
        public static List<T> BuildEntitiesToUpdate<T>(List<T> entities, List<T> newEntities) where T : class, IEntity
        {
            // Update those found in the list
            var entitiesToUpdate= new List<T>();
            foreach (var newEntity in newEntities)
            {
                var entityExpression = EntityExpression.Entity(newEntity.Id);
                if (entities.FirstOrDefault(entityExpression.Compile()) != null)
                    entitiesToUpdate.Add(newEntity);
            }

            // Return
            return entitiesToUpdate;
        }
        public static List<T> BuildEntitiesToRemove<T>(List<T> entities, List<T> newEntities) where T : class, IEntity
        {
            // Remove those no longer in the list
            var entitiesToRemove = new List<T>();
            foreach (var entity in entities)
            {
                var entityExpression = EntityExpression.Entity(entity.Id);
                if (newEntities.FirstOrDefault(entityExpression.Compile()) == null)
                    entitiesToRemove.Add(entity);
            }

            // Return
            return entitiesToRemove;
        }
    }
}
