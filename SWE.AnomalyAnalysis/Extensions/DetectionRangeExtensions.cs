namespace SWE.AnomalyAnalysis.Extensions
{
    using SWE.AnomalyAnalysis.Enums;
    using SWE.AnomalyAnalysis.Interfaces;
    using SWE.BasicType.Utilities;
    using System;

    internal static class DetectionRangeExtensions
    {
        internal static Anomaly Validate<TValue>(this IDetectionRange<TValue> range, TValue value)
            where TValue : IComparable<TValue>
        {
            if (CompareUtilities.GreaterThan(value, range.MaxDetectionHigh))
            {
                return Anomaly.High;
            }

            if (CompareUtilities.SmallerThan(value, range.MaxDetectionHigh))
            {
                return Anomaly.Low;
            }

            return Anomaly.None;
        }
    }
}