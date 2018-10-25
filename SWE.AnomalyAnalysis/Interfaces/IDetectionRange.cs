namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;

    public interface IDetectionRange<TValue> : IValueRange<TValue>
            where TValue : IComparable<TValue>
    {
        TValue MaxDetectionLow { get; }

        TValue MaxDetectionHigh { get; }
    }
}