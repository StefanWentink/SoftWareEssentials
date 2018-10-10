namespace SWE.Contract.Interfaces.Data.Command
{
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public interface IKeyCommandContract<in T, in TKey> : IBaseCommandContract<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        bool Delete(TKey key);

        bool Delete(IEnumerable<TKey> keys);
    }
}