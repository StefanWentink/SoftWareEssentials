namespace SWE.Builder.Interfaces
{
    /// <summary>
    /// Builder pattern.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    public interface IBuilder<out TOut, in TIn>
    {
        /// <summary>
        /// Constructs a <see cref="TOut"/>
        /// </summary>
        /// <returns></returns>
        TOut Build();
    }
}