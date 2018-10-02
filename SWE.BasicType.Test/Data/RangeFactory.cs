namespace SWE.BasicType.Test.Data
{
    using System;

    internal static class RangeFactory
    {
        internal static DateTime ToTestDateTime(int value)
        {
            return new DateTime(2018, 1, 2, 3, 4, value);
        }

        internal static DateTimeOffset ToTestDateTimeOffset(int value)
        {
            return new DateTimeOffset(ToTestDateTime(value));
        }

        internal static double ToTestDouble(double value)
        {
            return -1 + (value / 10);
        }
    }
}