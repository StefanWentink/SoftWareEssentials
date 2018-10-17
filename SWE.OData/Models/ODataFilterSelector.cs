namespace SWE.OData.Models
{
    using SWE.OData.Enums;
    using SWE.Reflection.Extensions;
    using System;
    using System.Linq.Expressions;

    public class ODataFilterSelector<T, TValue> : ODataFilter<TValue>
    {
        public ODataFilterSelector(Expression<Func<T, TValue>> selector, FilterOperator @operator, TValue value)
            : this(selector, @operator, value, string.Empty)
        {
        }

        public ODataFilterSelector(Expression<Func<T, TValue>> selector, FilterOperator @operator, TValue value, string entity)
            : base(selector.GetMemberInfo().Member.Name, @operator, value, entity)
        {
        }
    }
}