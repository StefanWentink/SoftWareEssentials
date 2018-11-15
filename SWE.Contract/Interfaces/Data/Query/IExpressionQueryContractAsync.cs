namespace SWE.Contract.Interfaces.Data.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IExpressionQueryContractAsync<T> : IQueryContract<T>
    {
        Task<IEnumerable<T>> ReadAsync(Expression<Func<T, bool>> expression);
    }
}