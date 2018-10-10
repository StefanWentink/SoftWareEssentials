namespace SWE.Contract.Interfaces.Data.Query
{
    using SWE.Model.Interfaces;
    using System;

    public interface IKeyQueryContract<T, in TKey> : IQueryContract<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        T Read(TKey key);
    }
}