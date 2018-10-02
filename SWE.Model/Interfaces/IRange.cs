namespace SWE.Model.Interfaces
{
    using System;

    public interface IRange<out TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// <see cref="IRange{TValue}"/> lower boundary (included).
        /// </summary>
        TValue From { get; }

        /// <summary>
        /// <see cref="IRange{TValue}"/> upper boundary (excluded).
        /// </summary>
        TValue Till { get; }
    }
}