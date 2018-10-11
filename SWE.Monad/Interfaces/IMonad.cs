namespace SWE.Monad.Interfaces
{
    public interface IMonad<T>
    {
        T Value { get; }

        /// <summary>
        /// Gets <see cref="Value"/>.
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetValue();

        /// <summary>
        /// Gets <see cref="Value"/> if not default or <see cref="@default"/>
        /// </summary>
        /// <param name="default"></param>
        /// <returns></returns>
        T GetValueOrDefault(T @default);
    }
}