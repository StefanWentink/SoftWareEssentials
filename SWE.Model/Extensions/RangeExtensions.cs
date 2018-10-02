namespace SWE.Model.Extensions
{
    using System;
    using System.Collections.Generic;
    using SWE.BasicType.Utilities;
    using SWE.Model.Interfaces;

    public static class RangeExtensions
    {
        /// <summary>
        /// Is <see cref="value"/> greater or equal to <see cref="IRange{TValue}.From"/> and smaller than <see cref="IRange{TValue}.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool InRange<TValue>(this IRange<TValue> range, TValue value)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerOrEqualTo(range.From, value) && CompareUtilities.GreaterThan(range.Till, value);
        }

        /// <summary>
        /// Is <see cref="value"/> greater than <see cref="IRange{TValue}.From"/> and smaller than <see cref="IRange{TValue}.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool InBetween<TValue>(this IRange<TValue> range, TValue value)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerThan(range.From, value) && CompareUtilities.GreaterThan(range.Till, value);
        }

        /// <summary>
        /// Do the ranges overlap.
        /// Is <see cref="range.From"/> smaller than <see cref="compare.Till"/>
        /// And <see cref="range.Till"/> greater than <see cref="compare.From"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool OverLaps<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerThan(range.From, compare.Till) && CompareUtilities.GreaterThan(range.Till, compare.From);
        }

        /// <summary>
        /// Is <see cref="range.From"/> equal to <see cref="compare.From"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool FromEquals<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return range.From.Equals(compare.From);
        }

        /// <summary>
        /// Is <see cref="range.Till"/> equal to <see cref="compare.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool TillEquals<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return range.Till.Equals(compare.Till);
        }

        /// <summary>
        /// Is <see cref="range.From"/> equal to <see cref="compare.From"/>
        /// Is <see cref="range.Till"/> equal to <see cref="compare.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool RangeEquals<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return range.FromEquals(compare) && range.TillEquals(compare);
        }

        /// <summary>
        /// Does the <see cref="range"/> equal or exceed the limits of <see cref="compare"/>
        /// Is <see cref="range.From"/> smaller or equal to <see cref="compare.From"/>
        /// Is <see cref="range.Till"/> greater or equal to <see cref="compare.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool Ecapsulates<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerOrEqualTo(range.From, compare.From) && CompareUtilities.GreaterOrEqualTo(range.Till, compare.Till);
        }

        /// <summary>
        /// Does the <see cref="range"/> equal or exceed the limits of <see cref="compare.From"/>
        /// Is <see cref="range.From"/> smaller or equal to <see cref="compare.From"/>
        /// Is <see cref="range.Till"/> greater than <see cref="compare.From"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool EcapsulatesFrom<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerOrEqualTo(range.From, compare.From) && CompareUtilities.GreaterThan(range.Till, compare.From);
        }

        /// <summary>
        /// Does the <see cref="range"/> equal or exceed the limits of <see cref="compare.Till"/>
        /// Is <see cref="range.From"/> smaller than <see cref="compare.Till"/>
        /// Is <see cref="range.Till"/> greater or equal to <see cref="compare.Till"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool EcapsulatesTill<TValue>(this IRange<TValue> range, IRange<TValue> compare)
            where TValue : IComparable<TValue>
        {
            return CompareUtilities.SmallerThan(range.From, compare.Till) && CompareUtilities.GreaterOrEqualTo(range.Till, compare.Till);
        }

        /// <summary>
        /// Returns the remainder of <see cref="range"/> after subtracting <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TRange"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="range"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static IEnumerable<TRange> Subtract<TRange, TValue>(
            this TRange range,
            IRange<TValue> compare)
            where TRange : IRangeWith<TRange, TValue>
            where TValue : IComparable<TValue>
        {
            if (!range.OverLaps(compare))
            {
                return new[] { range };
            }

            if (compare.Ecapsulates(range))
            {
                return new TRange[] { };
            }

            if (compare.EcapsulatesFrom(range))
            {
                return new TRange[] { range.With(compare.Till, range.Till) };
            }

            if (compare.EcapsulatesTill(range))
            {
                return new TRange[] { range.With(range.From, compare.From) };
            }

            return new TRange[]
            {
                range.With(range.From, compare.From),
                range.With(compare.Till, range.Till)
            };
        }
    }
}