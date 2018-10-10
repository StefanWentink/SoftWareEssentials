namespace SWE.Contract.Interfaces.Data.Command
{
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IKeyCommandContractAsync<in T, in TKey> : IBaseCommandContractAsync<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> DeleteAsync(TKey keys);

        Task<bool> DeleteAsync(IEnumerable<TKey> keys);
    }
}