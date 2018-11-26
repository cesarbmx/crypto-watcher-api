using System;
using System.Linq;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Specifications
{
    public abstract class Specification<TEntity> where TEntity : Entity
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
    public class AndSpecification<TEntity> : Specification<TEntity> where TEntity : Entity
    {
        private readonly Specification<TEntity> _left;
        private readonly Specification<TEntity> _right;

        public AndSpecification(Specification<TEntity> left, Specification<TEntity> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var andExpression = Expression.AndAlso(
                leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TEntity, bool>>(
                andExpression, leftExpression.Parameters.Single());
        }
    }
}
