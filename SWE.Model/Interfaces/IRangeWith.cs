namespace SWE.Model.Interfaces
{
    using System;

    public interface IRangeWith<TResult, TValue> : IRange<TValue>
        where TResult : IRange<TValue>
        where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Returns a new instance of <see cref="TResult"/> with values <see cref="from"/> and <see cref="till"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="till"></param>
        /// <returns></returns>
        TResult With(TValue from, TValue till);
    }
}