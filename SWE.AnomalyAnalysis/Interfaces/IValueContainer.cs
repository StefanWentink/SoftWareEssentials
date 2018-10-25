namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IValueContainer<TValue>
            where TValue : IComparable<TValue>
    {
        Task AddAsync(TValue value);
    }
}