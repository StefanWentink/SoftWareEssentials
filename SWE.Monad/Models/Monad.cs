namespace SWE.Monad.Models
{
    using SWE.Monad.Interfaces;

    public abstract class Monad<T> : IMonad<T>
    {
        public T Value { get; }

        protected Monad()
        {
        }

        protected Monad(T value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public virtual T GetValue()
        {
            return Value;
        }

        /// <inheritdoc/>
        public virtual T GetValueOrDefault(T @default)
        {
            return Equals(GetValue(), default(T))
                ? @default
                : GetValue();
        }
    }
}