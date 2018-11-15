namespace SWE.Async.Extensions
{
    using System;
    using System.Threading.Tasks;

    public static class AsyncExtensions
    {
        /// <summary>
        /// Maps any <see cref="TSource"/> to async.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Task<TSource> Map<TSource>(TSource source)
        {
            return Task.FromResult(source);
        }

        /// <summary>
        /// Maps any <see cref="Task{TSource}"/> to <see cref="Task{TResult}"/> through <see cref="mapFunction"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sourceTask"></param>
        /// <param name="mapFunction"></param>
        /// <returns></returns>
        public static async Task<TResult> MapAsync<TSource, TResult>(
            this Task<TSource> sourceTask,
            Func<TSource, Task<TResult>> mapFunction)
        {
            return await mapFunction(await sourceTask.ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}