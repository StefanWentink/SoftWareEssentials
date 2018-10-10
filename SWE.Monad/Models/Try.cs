namespace SWE.Monad.Models
{
    using Interfaces;
    using System;

    public abstract class Try<T> : Monad<T>, ITry<T>
    {
        public virtual bool IsFailure => Exception != null;

        public Exception Exception { get; protected set; }

        protected Try(Exception exception)
            : base(default)
        {
            if (exception != null)
            {
                Exception = exception;
            }
        }

        protected Try(T instance)
            : base(instance)
        {
        }

        protected Try()
        {
        }

        /// <summary>
        /// Maps <see cref="T"/> to <see cref="ITry{TResult}"/> by invoking<see cref="mapper"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns>Returns <see cref="Models.Success{TResult}"/>.
        /// Returns <see cref="Models.Failure{TResult}"/> holding any exception.</returns>
        public static ITry<TResult> Invoke<TResult>(Func<TResult> mapper)
        {
            try
            {
                return new Success<TResult>(mapper.Invoke());
            }
            catch (Exception exception)

            {
                return new Failure<TResult>(exception);
            }
        }

        /// <inheritdoc/>
        public abstract ITry<TResult> Map<TResult>(Func<T, ITry<TResult>> mapper);

        /// <inheritdoc/>
        public abstract ITry<TResult> Map<TResult>(Func<T, TResult> mapper);

        /// <inheritdoc/>
        public abstract ITry<T> Recover(Func<Exception, T> recover);

        /// <inheritdoc/>
        public abstract ITry<T> Recover<TException>(Func<Exception, T> recover)
            where TException : Exception;

        /// <inheritdoc/>
        public abstract ITry<T> Filter(Predicate<T> predicate);

        /// <inheritdoc/>
        public abstract ITry<T> Execute(Action<T> action);
    }
}