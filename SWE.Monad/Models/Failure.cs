namespace SWE.Monad.Models
{
    using SWE.Monad.Interfaces;
    using System;

    public class Failure<T> : Try<T>
    {
        public override bool IsFailure { get; } = true;

        public Failure(Exception exception)
        {
            Exception = exception;
        }

        /// <inheritdoc/>
        public override T GetValueOrDefault(T @default)
        {
            return @default;
        }

        /// <inheritdoc/>
        public override ITry<TResult> Map<TResult>(Func<T, TResult> mapper)
        {
            return new Failure<TResult>(Exception);
        }

        /// <inheritdoc/>
        public override ITry<TResult> Map<TResult>(Func<T, ITry<TResult>> mapper)
        {
            return new Failure<TResult>(Exception);
        }

        /// <inheritdoc/>
        public override ITry<T> Recover(Func<Exception, T> recover)
        {
            try
            {
                return new Success<T>(recover.Invoke(Exception));
            }
            catch
            {
                return new Failure<T>(Exception);
            }
        }

        /// <inheritdoc/>
        public override ITry<T> Recover<TException>(Func<Exception, T> recover)
        {
            return Exception.GetType().IsAssignableFrom(typeof(TException))
                ? Recover(recover)
                : this;
        }

        /// <inheritdoc/>
        public override ITry<T> Filter(Predicate<T> predicate)
        {
            return this;
        }

        /// <inheritdoc/>
        public override ITry<T> Execute(Action<T> action)
        {
            return this;
        }
    }
}
