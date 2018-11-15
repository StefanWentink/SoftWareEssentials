namespace SWE.Contract.Interfaces.Data.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IExpressionQueryContract<T> : IQueryContract<T>
    {
        IEnumerable<T> Read(Expression<Func<T, bool>> expression);
    }
}