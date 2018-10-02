namespace SWE.Contract.Interfaces.Data.Query
{
    using System;

    using SWE.Model.Interfaces;

    public interface IKeyQueryContract<T, in TKey> : IQueryContract<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        T Read(TKey key);
    }
}