using System;
using System.Linq.Expressions;
using CryptoWatcher.Shared.Models;

namespace CryptoWatcher.Shared.Expressions
{
    public static class LogExpression
    {
        public static Expression<Func<Log, bool>> AuditLog(DateTime dateTime)
        {
            return x => x.Time < dateTime;
        }
    }
}
