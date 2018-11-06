namespace SWE.EntityFramework.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public static class AsyncExtensions
    {
        /// <summary>
        /// Handle <see cref="Enumerable.ToArray"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T[]> ToArrayAsyncSafe<T>(
            this IQueryable<T> query,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Enumerable.ToArray,
                EntityFrameworkQueryableExtensions.ToArrayAsync,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Enumerable.ToList"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<List<T>> ToListAsyncSafe<T>(
            this IQueryable<T> query,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Enumerable.ToList,
                EntityFrameworkQueryableExtensions.ToListAsync,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.SingleOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> SingleOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.SingleOrDefault,
                EntityFrameworkQueryableExtensions.SingleOrDefaultAsync,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.SingleOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> SingleOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.SingleOrDefault,
                EntityFrameworkQueryableExtensions.SingleOrDefaultAsync,
                predicate,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.FirstOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> FirstOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.FirstOrDefault,
                EntityFrameworkQueryableExtensions.FirstOrDefaultAsync,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.FirstOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> FirstOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.FirstOrDefault,
                EntityFrameworkQueryableExtensions.FirstOrDefaultAsync,
                predicate,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.LastOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> LastOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.LastOrDefault,
                EntityFrameworkQueryableExtensions.LastOrDefaultAsync,
                cancellationToken);
        }

        /// <summary>
        /// Handle <see cref="Queryable.LastOrDefault"/> on any <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="query"/> is null.</exception>
        public static Task<T> LastOrDefaultAsyncSafe<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return query.ResolveSafe(
                Queryable.LastOrDefault,
                EntityFrameworkQueryableExtensions.LastOrDefaultAsync,
                predicate,
                cancellationToken);
        }

        /// <summary>
        /// If null throw ArgumentNullException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        internal static void ExceptionIfNull<T>(IQueryable<T> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
        }

        internal static Task<TResult> ResolveSafe<T, TResult>(
            this IQueryable<T> query,
            Func<IQueryable<T>, TResult> expressionSync,
            Func<IQueryable<T>, CancellationToken, Task<TResult>> expressionAsync,
            CancellationToken cancellationToken = default)
        {
            ExceptionIfNull(query);

            if (!(query is IAsyncEnumerable<T>))
            {
                return Task.FromResult(expressionSync(query));
            }

            return expressionAsync(query, cancellationToken);
        }

        internal static Task<TResult> ResolveSafe<T, TResult>(
            this IQueryable<T> query,
            Func<IQueryable<T>, Expression<Func<T, bool>>, TResult> expressionSync,
            Func<IQueryable<T>, Expression<Func<T, bool>>, CancellationToken, Task<TResult>> expressionAsync,
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            ExceptionIfNull(query);

            if (!(query is IAsyncEnumerable<T>))
            {
                return Task.FromResult(expressionSync(query, predicate));
            }

            return expressionAsync(query, predicate, cancellationToken);
        }
    }
}