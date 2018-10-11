namespace SWE.Contract.Interfaces.Data.Query
{
    using SWE.Model.Interfaces;
    using System;
    using System.Threading.Tasks;

    public interface IKeyQueryContractAsync<T, in TKey> : IQueryContractAsync<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<T> ReadAsync(TKey key);
    }
}