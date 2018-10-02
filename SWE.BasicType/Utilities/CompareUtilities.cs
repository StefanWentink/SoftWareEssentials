namespace SWE.BasicType.Utilities
{
    using System;

    public static class CompareUtilities
    {
        /// <summary>
        /// Is <see cref="value"/> equal to <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool Equals<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) == 0;
        }

        /// <summary>
        /// Is <see cref="value"/> equal to <see cref="compare"/> up to <see cref="tolerance"/> digits.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool EqualsWithinTolerance(double value, double compare, int tolerance)
        {
            var difference = Math.Abs(value - compare) * Math.Pow(10, tolerance);
            var roundedDifference = Math.Round(Math.Round(difference * 10) / 10);
            return roundedDifference == 0;
        }

        /// <summary>
        /// Is <see cref="value"/> greater or equal to <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool GreaterOrEqualTo<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) >= 0;
        }

        /// <summary>
        /// Is <see cref="value"/> greater than <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool GreaterThan<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) > 0;
        }

        /// <summary>
        /// Is <see cref="value"/> smaller or equal to <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool SmallerOrEqualTo<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) <= 0;
        }

        /// <summary>
        /// Is <see cref="value"/> smaller than <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool SmallerThan<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) < 0;
        }

        /// <summary>
        /// Returns the smallest of <see cref="value"/> and <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static TValue Max<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) < 0
                ? compare
                : value;
        }

        /// <summary>
        /// Returns the smallest of <see cref="value"/> and <see cref="compare"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static TValue Min<TValue>(TValue value, TValue compare)
            where TValue : IComparable<TValue>
        {
            return value.CompareTo(compare) < 0
                ? value
                : compare;
        }

        /// <summary>
        /// Determines if <see cref="value"/> is default for <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(T value)
        {
            return object.Equals(value, default(T));
        }
    }
}