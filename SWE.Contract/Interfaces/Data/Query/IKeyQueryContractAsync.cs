namespace SWE.Contract.Interfaces.Data.Query
{
    using SWE.Model.Interfaces;
    using System;

    public interface IKeyQueryContractAsync<T, in TKey> : IQueryContractAsync<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        T ReadAsync(TKey key);
    }
}