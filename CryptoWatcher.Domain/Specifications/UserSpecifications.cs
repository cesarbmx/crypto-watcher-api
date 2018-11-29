using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Specifications
{
    public class IdSpecification : Specification<User>
    {
        private readonly string _userId;

        public IdSpecification(string userId)
        {
            _userId = userId;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return x => x.Id == _userId;
        }
    }
   
}
