using System;
using System.Linq.Expressions;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Domain.Expressions
{
    public static class LogExpression
    {
        public static Expression<Func<Log, bool>> AuditLog(DateTime dateTime)
        {
            return x => x.Time < dateTime;
        }
    }
}
