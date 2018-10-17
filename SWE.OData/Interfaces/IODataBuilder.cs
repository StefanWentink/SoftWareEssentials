namespace SWE.OData.Interfaces
{
    using SWE.OData.Builders;
    using System;
    using System.Linq.Expressions;

    public interface IODataBuilder<T, TKey> : IODataBasicBuilder
        where TKey : IComparable<TKey>
    {
        ODataBuilder<T, TKey> Expand(IODataEntityBuilder subEntity);

        ODataBuilder<T, TKey> SetFilter(IODataFilters filters);

        ODataBuilder<T, TKey> SetFilter(IODataFilter filter);

        ODataBuilder<T, TKey> SetOrder(string order);

        ODataBuilder<T, TKey> SetOrder<TOrder, TValue>(Expression<Func<TOrder, TValue>> order);

        ODataBuilder<T, TKey> SetDescending(bool descending);

        ODataBuilder<T, TKey> SetTop(int top);

        ODataBuilder<T, TKey> SetSkip(int skip);
    }
}