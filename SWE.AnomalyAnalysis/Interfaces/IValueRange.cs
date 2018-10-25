namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;

    public interface IValueRange<TValue>
            where TValue : IComparable<TValue>
    {
        TValue Low { get; }

        TValue Normal { get; }

        TValue High { get; }
    }
}