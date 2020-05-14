using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class UserExpression
    {
        public static Expression<Func<User, bool>> User(string userId = null)
        {
            return x =>  string.IsNullOrEmpty(userId) || x.UserId == userId;
        }       
    }
}
