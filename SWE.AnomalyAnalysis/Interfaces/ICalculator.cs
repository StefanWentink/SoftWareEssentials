namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ICalculator<TValue>
            where TValue : IComparable<TValue>
    {
        IDetectionRange<TValue> Calculate(IEnumerable<TValue> values, IEnumerable<TValue> anomalies);
    }
}