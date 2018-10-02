namespace SWE.Builder.Interfaces
{
    /// <summary>
    /// Member builder pattern
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <typeparam name="TIn"></typeparam>
    public interface IMemberBuilder<out TOut, in TIn> : IBuilder<TOut, TIn>
        where TOut : TIn
    {
        /// <summary>
        /// Sets a <see cref="TIn"/>
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IBuilder<TOut, TIn> SetMember(TIn member);
    }
}