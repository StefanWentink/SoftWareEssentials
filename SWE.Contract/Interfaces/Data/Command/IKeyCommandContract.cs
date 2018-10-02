namespace SWE.Contract.Interfaces.Data.Command
{
    using System;
    using System.Collections.Generic;

    using SWE.Model.Interfaces;

    public interface IKeyCommandContract<in T, in TKey> : IBaseCommandContract<T>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
    {
        bool Delete(TKey key);

        bool Delete(IEnumerable<TKey> keys);
    }
}