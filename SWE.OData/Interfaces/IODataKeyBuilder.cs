namespace SWE.OData.Interfaces
{
    using System;

    public interface IODataKeyBuilder<T, TKey> : IODataEntityBuilder
        where TKey : IComparable<TKey>
    {
        TKey Key { get; }
    }
}