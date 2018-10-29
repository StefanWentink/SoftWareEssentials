namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using SWE.BasicType.Utilities;
    using System;

    public class ValueRange<TValue> : IValueRange<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue Low { get; }

        public TValue Normal { get; }

        public TValue High { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectionRange"/> class.
        /// </summary>
        /// <param name="low"></param>
        /// <param name="normal"></param>
        /// <param name="high"></param>
        /// <exception cref="ArgumentException">If <see cref=low"/> greater than <see cref="normal"/>.</exception>
        /// <exception cref="ArgumentException">If <see cref="high"/> smaller than <see cref="normal"/>.</exception>
        public ValueRange(TValue low, TValue normal, TValue high)
        {
            if (!CompareUtilities.SmallerOrEqualTo(low, normal))
            {
                throw new ArgumentException($"{nameof(low)} must be smaller or equal to {normal}.", nameof(low));
            }

            if (!CompareUtilities.GreaterOrEqualTo(high, normal))
            {
                throw new ArgumentException($"{nameof(high)} must be smaller or equal to {normal}.", nameof(high));
            }

            Low = low;
            Normal = normal;
            High = high;
        }
    }
}