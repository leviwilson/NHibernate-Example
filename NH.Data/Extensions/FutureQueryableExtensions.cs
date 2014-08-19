using System;
using System.Linq;
using NH.Data.Linq;

namespace NH.Data.Extensions
{
    public static class FutureQueryableExtensions
    {
        public static FutureQueryable<TEntity> AddBatch<TEntity>(this IQueryable<TEntity> queryable, Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return new FutureQueryable<TEntity>(queryable)
                .AddBatch(func);
        }
    }
}
