namespace SWE.BasicType.Date.Utilities
{
    using System;

    public static class ConversionUtilities
    {
        /// <summary>
        /// Returns DateTimeOffset in utc.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTimeOffset UnixTimeStampToDateTimeOffset(double value)
        {
            return UnixTimeStampToDateTimeOffset(value, 0);
        }

        /// <summary>
        /// Returns DateTimeOffset with given <see cref="offsetSeconds"/>
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <param name="offsetSeconds"></param>
        /// <returns></returns>
        public static DateTimeOffset UnixTimeStampToDateTimeOffset(double value, int offsetSeconds)
        {
            var datetime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(offsetSeconds);
            var result = new DateTimeOffset(datetime, TimeSpan.FromSeconds(offsetSeconds));
            return result.AddSeconds(value);
        }

        /// <summary>
        /// Returns UnixTimeStamp with given <see cref="offsetSeconds"/>
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <param name="offsetSeconds"></param>
        /// <returns></returns>
        public static double DateTimeOffsetToUnixTimeStamp(DateTimeOffset value)
        {
            return (value - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
        }
    }
}