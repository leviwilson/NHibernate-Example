using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace NH.Data.Linq
{
    public class FutureQueryable<TEntity> : IEnumerable<TEntity>
    {
        private readonly IQueryable<TEntity> _original;

        public FutureQueryable(IQueryable<TEntity> original)
        {
            _original = original;
        }

        public FutureQueryable<TEntity> AddBatch(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            LastFuture = func(_original).ToFuture();
            return this;
        }

        public FutureQueryable<TEntity> AddBatch<THqlEntity>(IQuery hqlQuery)
        {
            /** *  Do not preserve the LastFuture property. HQL Queries should be used sparingly and often are
             *  indepdendent from the _original IQueryable<TEntity>. This is simply a wrapper around the Future<T> method
             *  of IQuery, but to make clear that it is still part of this batch.
             **/
            hqlQuery.Future<THqlEntity>(); // do not 
            return this;
        }

        public IEnumerable<TEntity> LastFuture { get; private set; }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return LastFuture.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
