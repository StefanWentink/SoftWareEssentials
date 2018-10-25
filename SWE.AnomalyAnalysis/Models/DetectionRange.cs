namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using System;

    public class DetectionRange<TValue> : ValueRange<TValue>, IDetectionRange<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue MaxDetectionLow { get; }

        public TValue MaxDetectionHigh { get; }

        public DetectionRange(TValue maxDetectionLow, TValue low, TValue normal, TValue high, TValue maxDetectionHigh)
            : base(low, normal, high)
        {
            MaxDetectionLow = maxDetectionLow;
            MaxDetectionHigh = maxDetectionHigh;
        }
    }
}