namespace SWE.Monad.Models
{
    using SWE.Monad.Interfaces;
    using SWE.Monad.Utilities;
    using System;

    public class Success<T> : Try<T>
    {
        public Success(T instance)
            : base(instance)
        {
        }

        /// <inheritdoc/>
        public override ITry<TResult> Map<TResult>(Func<T, TResult> mapper)
        {
            if (mapper == null)
            {
                return new Failure<TResult>(new ArgumentNullException(nameof(mapper), $"{nameof(mapper)} not defined"));
            }

            try
            {
                return MonadUtilities.ReturnTry(mapper(Value));
            }
            catch (Exception ex)
            {
                return new Failure<TResult>(ex);
            }
        }

        /// <inheritdoc/>
        public override ITry<TResult> Map<TResult>(Func<T, ITry<TResult>> mapper)
        {
            if (mapper == null)
            {
                return new Failure<TResult>(new ArgumentNullException(nameof(mapper), $"{nameof(mapper)} not defined"));
            }

            try
            {
                return mapper(Value);
            }
            catch (Exception exception)
            {
                return new Failure<TResult>(exception);
            }
        }

        /// <inheritdoc/>
        public override ITry<T> Recover(Func<Exception, T> recover)
        {
            return this;
        }

        /// <inheritdoc/>
        public override ITry<T> Recover<TException>(Func<Exception, T> recover)
        {
            return this;
        }

        /// <inheritdoc/>
        public override ITry<T> Filter(Predicate<T> predicate)
        {
            try
            {
                if (predicate.Invoke(Value))
                {
                    return this;
                }

                return new Failure<T>(new Exception($"There are no results from applying the {nameof(predicate)}"));
            }
            catch (Exception exception)
            {
                return new Failure<T>(exception);
            }
        }

        /// <inheritdoc/>
        public override ITry<T> Execute(Action<T> action)
        {
            try
            {
                action.Invoke(Value);

                return this;
            }
            catch (Exception exception)
            {
                return new Failure<T>(exception);
            }
        }
    }
}