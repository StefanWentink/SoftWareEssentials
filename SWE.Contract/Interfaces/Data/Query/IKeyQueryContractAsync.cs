namespace SWE.Contract.Interfaces.Data.Query
{
    using System;

    using SWE.Model.Interfaces;

    public interface IKeyQueryContractAsync<T, in TKey> : IQueryContractAsync<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        T ReadAsync(TKey key);
    }
}