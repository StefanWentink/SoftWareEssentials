namespace SWE.BasicType.Date.Extensions
{
    using SWE.BasicType.Date.Utilities;
    using System;

    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from it's current timezone to <see cref="TimeZoneInfo.Utc"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTimeOffset ToUtcTimeZone(this DateTimeOffset value)
        {
            return value.UtcDateTime.ToDateTimeOffset(TimeZoneInfo.Utc);
        }

        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from <see cref="TimeZoneInfo.Utc"/> to <see cref="timeZoneInfo"/>
        /// disregarding it's own offset.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static DateTimeOffset ToTimeZoneFromUtc(this DateTimeOffset value, TimeZoneInfo timeZoneInfo)
        {
            return value.ToTimeZone(TimeZoneInfo.Utc, timeZoneInfo);
        }

        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from <see cref="TimeZoneInfo.Local"/> to <see cref="timeZoneInfo"/>
        /// disregarding it's own offset
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static DateTimeOffset FromLocalToTimeZone(this DateTimeOffset value, TimeZoneInfo timeZoneInfo)
        {
            return value.ToTimeZone(TimeZoneInfo.Local, timeZoneInfo);
        }

        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from <see cref="TimeZoneInfoUtilities.DutchTimeZoneInfo"/> to <see cref="timeZoneInfo"/>
        /// disregarding it's own offset
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static DateTimeOffset FromDutchToTimeZone(this DateTimeOffset value, TimeZoneInfo timeZoneInfo)
        {
            return value.ToTimeZone(TimeZoneInfoUtilities.DutchTimeZoneInfo, timeZoneInfo);
        }

        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from <see cref="sourceTimeZone"/> to <see cref="destinationTimeZone"/>
        /// disregarding it's own offset
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sourceTimeZone"></param>
        /// <param name="destinationTimeZone"></param>
        /// <returns></returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset value, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        {
            return value.DateTime.ToDateTimeOffset(sourceTimeZone, destinationTimeZone);
        }

        /// <summary>
        /// Transfers a <see cref="DateTimeOffset"/> from it's current timezone to <see cref="timeZoneInfo"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset value, TimeZoneInfo timeZoneInfo)
        {
            return value.ToUtcTimeZone().ToTimeZoneFromUtc(timeZoneInfo);
        }

        /// <summary>
        /// Set the time of day to 00:00:00.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTimeOffset SetToStartOfDay(this DateTimeOffset value)
        {
            return value.SetToTimeOfDay(0, 0, 0);
        }

        /// <summary>
        /// Set the time of day to a <see cref="hour"/>:<see cref="minute"/>:<see cref="second"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTimeOffset SetToTimeOfDay(this DateTimeOffset value, int hour, int minute, int second)
        {
            var timespanDifference = value.DateTime - value.Date.AddHours(hour).AddMinutes(minute).AddSeconds(second);

            return value - timespanDifference;
        }

        /// <summary>
        /// Sets the time to the nearest factor of <see cref="timeSpan"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static DateTimeOffset Round(this DateTimeOffset value, TimeSpan timeSpan) => value.DoRound(timeSpan, null);

        /// <summary>
        /// Sets the time to the previous factor of <see cref="timeSpan"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static DateTimeOffset RoundDown(this DateTimeOffset value, TimeSpan timeSpan) => value.DoRound(timeSpan, false);

        /// <summary>
        /// Sets the time to the next factor of <see cref="timeSpan"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static DateTimeOffset RoundUp(this DateTimeOffset value, TimeSpan timeSpan) => value.DoRound(timeSpan, true);

        /// <summary>
        /// Sets the time to the previous factor of <see cref="timeSpan"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <param name="roundUp"></param>
        /// <returns></returns>
        private static DateTimeOffset DoRound(this DateTimeOffset value, TimeSpan timeSpan, bool? roundUp)
        {
            if (timeSpan.Ticks == 0)
            {
                return value;
            }

            var (previous, next) = value.GetDeltaTicks(timeSpan);

            return roundUp ?? (next <= previous)
                       ? (next == 0 ? value : value.AddTicks(next))
                       : (previous == 0 ? value : value.AddTicks(-previous));
        }

        /// <summary>
        /// Sets the time to the previous factor of <see cref="timeSpan"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private static (long previous, long next) GetDeltaTicks(this DateTimeOffset value, TimeSpan timeSpan)
        {
            if (timeSpan.Ticks == 0)
            {
                return (0, 0);
            }

            var ticks = value.Ticks;

            var previous = ticks % timeSpan.Ticks;
            var next = timeSpan.Ticks - previous;

            return (previous, next);
        }
    }
}