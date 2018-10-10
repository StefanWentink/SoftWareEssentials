namespace SWE.BasicType.Date.Test.Extensions
{
    using global::Xunit;
    using SWE.BasicType.Date.Extensions;
    using SWE.BasicType.Date.Utilities;
    using SWE.Xunit.Attributes;
    using System;

    public class DateTimeExtensionsTest
    {
        private static readonly DateTime _winterTime = new DateTime(2018, 1, 1);

        private static readonly DateTime _summerTime = new DateTime(2018, 7, 1);

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetLocal_Returns_SummerTime()
        {
            AssertDateTimeOffset(_summerTime, DateTimeExtensions.ToDateTimeOffsetLocal, TimeZoneInfo.Local, true);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetLocal_Returns_WinterTime()
        {
            AssertDateTimeOffset(_winterTime, DateTimeExtensions.ToDateTimeOffsetLocal, TimeZoneInfo.Local, false);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetUtc_Returns_SummerTime()
        {
            AssertDateTimeOffset(_summerTime, DateTimeExtensions.ToDateTimeOffsetUtc, TimeZoneInfo.Utc, true);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetUtc_Returns_WinterTime()
        {
            AssertDateTimeOffset(_winterTime, DateTimeExtensions.ToDateTimeOffsetUtc, TimeZoneInfo.Utc, false);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetDutch_Returns_SummerTime()
        {
            AssertDateTimeOffset(_summerTime, DateTimeExtensions.ToDateTimeOffsetDutch, TimeZoneInfoUtilities.DutchTimeZoneInfo, true);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetDutch_Returns_WinterTime()
        {
            AssertDateTimeOffset(_winterTime, DateTimeExtensions.ToDateTimeOffsetDutch, TimeZoneInfoUtilities.DutchTimeZoneInfo, false);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetTimeZone_Returns_SummerTimeInDestinationTimeZone()
        {
            var dateTimeOffset = _summerTime.ToDateTimeOffset(TimeZoneInfoUtilities.DutchTimeZoneInfo, TimeZoneInfo.Utc);
            var expectedDestinationDate = _summerTime.AddHours(-2);
            Assert.Equal(expectedDestinationDate.Date, dateTimeOffset.Date);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void ToDateTimeOffsetTimeZone_Returns_WinterTimeInDestinationTimeZone()
        {
            var dateTimeOffset = _winterTime.ToDateTimeOffset(TimeZoneInfo.Utc, TimeZoneInfoUtilities.DutchTimeZoneInfo);
            var expectedDestinationDate = _winterTime.AddHours(1);
            Assert.Equal(expectedDestinationDate.Date, dateTimeOffset.Date);
        }

        [Fact]
        [Category("DateTimeExtensions")]
        public void GetAge_Should_ReturnAgeInYears()
        {
            var expected = 12;
            var value = new DateTime(2000, 2, 29);
            var compare = value.AddYears(expected);

            var actual = value.GetAge(compare);
            Assert.Equal(expected, actual);

            actual = value.GetAge(compare.AddDays(-1));
            Assert.Equal(expected - 1, actual);
        }

        private void AssertDateTimeOffset(
            DateTime dateTime,
            Func<DateTime, DateTimeOffset> function,
            TimeZoneInfo timeZoneInfo,
            bool summerTime)
        {
            var dateTimeOffset = function.Invoke(dateTime);
            Assert.Equal(dateTime, dateTimeOffset.Date);

            var expected = timeZoneInfo.BaseUtcOffset.TotalMinutes;

            if (summerTime)
            {
                expected += timeZoneInfo.GetAdjustmentRuleTimeSpan(dateTime).TotalMinutes;
            }

            var actual = (new DateTimeOffset(dateTime, TimeSpan.Zero) - dateTimeOffset).TotalMinutes;
            Assert.Equal(expected, actual);
        }
    }
}