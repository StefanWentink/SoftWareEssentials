namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using SWE.BasicType.Utilities;
    using System;

    public class DetectionRange<TValue> : ValueRange<TValue>, IDetectionRange<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue MaxDetectionLow { get; }

        public TValue MaxDetectionHigh { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectionRange"/> class.
        /// </summary>
        /// <param name="maxDetectionLow"></param>
        /// <param name="low"></param>
        /// <param name="normal"></param>
        /// <param name="high"></param>
        /// <param name="maxDetectionHigh"></param>
        /// <exception cref="ArgumentException">If <see cref="MaxDetectionLow"/> greater than <see cref="low"/>.</exception>
        /// <exception cref="ArgumentException">If <see cref="MaxDetectionHigh"/> smaller than <see cref="high"/>.</exception>
        public DetectionRange(TValue maxDetectionLow, TValue low, TValue normal, TValue high, TValue maxDetectionHigh)
            : base(low, normal, high)
        {
            if (!CompareUtilities.SmallerOrEqualTo(maxDetectionLow, low))
            {
                throw new ArgumentException($"{nameof(maxDetectionLow)} must be smaller or equal to {low}.", nameof(maxDetectionLow));
            }

            if (!CompareUtilities.GreaterOrEqualTo(maxDetectionHigh, high))
            {
                throw new ArgumentException($"{nameof(maxDetectionHigh)} must be smaller or equal to {high}.", nameof(maxDetectionHigh));
            }

            MaxDetectionLow = maxDetectionLow;
            MaxDetectionHigh = maxDetectionHigh;
        }
    }
}