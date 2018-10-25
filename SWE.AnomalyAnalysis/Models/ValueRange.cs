namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using System;

    public class ValueRange<TValue> : IValueRange<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue Low { get; }

        public TValue Normal { get; }

        public TValue High { get; }

        public ValueRange(TValue low, TValue normal, TValue high)
        {
            Low = low;
            Normal = normal;
            High = high;
        }
    }
}