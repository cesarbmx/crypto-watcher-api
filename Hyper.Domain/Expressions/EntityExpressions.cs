using System;
using System.Linq.Expressions;
using Hyper.Domain.Models;

namespace Hyper.Domain.Expressions
{
    public static class EntityExpressions
    {
        public static Expression<Func<Entity, bool>> HasKey(string id)
        {
            return x => x.Id == id;
        }
    }
}
