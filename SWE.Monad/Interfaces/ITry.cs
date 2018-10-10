namespace SWE.Monad.Interfaces
{
    using System;

    /// <summary>
    /// Holding a <see cref="Models.Success{T}"/> or <see cref="Models.Failure{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITry<T> : IMonad<T>
    {
        bool IsFailure { get; }

        Exception Exception { get; }

        /// <summary>
        /// Maps <see cref="T"/> to <see cref="ITry{TResult}"/> by invoking<see cref="mapper"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> holding any exception.</returns>
        ITry<TResult> Map<TResult>(Func<T, ITry<TResult>> mapper);

        /// <summary>
        /// Maps <see cref="T"/> to <see cref="TResult"/> by invoking <see cref="mapper"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> holding any exception.</returns>
        ITry<TResult> Map<TResult>(Func<T, TResult> mapper);

        /// <summary>
        /// Recover from <see cref="Exception"/> to <see cref="ITry{T}"/>
        /// </summary>
        /// <param name="recover"></param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> holding any exception.</returns>
        ITry<T> Recover(Func<Exception, T> recover);

        /// <summary>
        /// Recover from <see cref="TException"/> to <see cref="ITry{T}"/>
        /// </summary>
        /// <param name="recover"></param>
        /// <returns></returns>
        ITry<T> Recover<TException>(Func<Exception, T> recover)
            where TException : Exception;

        /// <summary>
        /// Execute <see cref="predicate"/> on <see cref="T"/>.
        /// </summary>
        /// <param name="predicate">Predicate executed on <see cref="T"/>.<param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> if <see cref="predicate"/> returns false.</returns>
        ITry<T> Filter(Predicate<T> predicate);

        /// <summary>
        /// Execute <see cref="action"/> on <see cref="T"/>.
        /// </summary>
        /// <param name="action">Action executed on <see cref="T"/>.</param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> holding any exception.</returns>
        ITry<T> Execute(Action<T> action);
    }
}