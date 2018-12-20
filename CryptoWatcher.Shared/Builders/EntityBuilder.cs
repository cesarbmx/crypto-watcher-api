using System.Collections.Generic;
using System.Linq;
using CryptoWatcher.Shared.Expressions;
using CryptoWatcher.Shared.Models;


namespace CryptoWatcher.Shared.Builders
{
    public static class EntityBuilder
    {
        public static List<T> BuildEntitiesToAdd<T>(List<T> entities, List<T> newEntities) where T : class, IEntity
        {
            // Add those not found in the list
            var entitiesToAdd = new List<T>();
            foreach (var newEntity in newEntities)
            {
                if (entities.FirstOrDefault(EntityExpression.Entity(newEntity.Id).Compile()) == null)
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
                if (entities.FirstOrDefault(EntityExpression.Entity(newEntity.Id).Compile()) != null)
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
                if (newEntities.FirstOrDefault(EntityExpression.Entity(entity.Id).Compile()) == null)
                    entitiesToRemove.Add(entity);
            }

            // Return
            return entitiesToRemove;
        }
    }
}
