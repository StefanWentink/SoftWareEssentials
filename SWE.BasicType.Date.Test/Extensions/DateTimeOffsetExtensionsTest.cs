namespace SWE.BasicType.Date.Test.Extensions
{
    using global::Xunit;
    using SWE.BasicType.Date.Extensions;
    using SWE.Xunit.Attributes;
    using System;

    public class DateTimeOffsetExtensionsTest
    {
        private static readonly DateTimeOffset _winterTime = new DateTimeOffset(2018, 1, 1, 12, 52, 30, 500, TimeSpan.FromHours(1));

        private static readonly DateTimeOffset _summerTime = new DateTimeOffset(2018, 7, 1, 1, 22, 29, 449, TimeSpan.FromHours(2));

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void SetToStartOfDay_Should_ResetTimeToMidnight()
        {
            var actual = _winterTime.SetToStartOfDay();
            var expected = new DateTimeOffset(new DateTime(2018, 1, 1), TimeSpan.FromHours(1));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void SetToTimeOfDay_Should_ResetTime()
        {
            var actual = _summerTime.SetToTimeOfDay(14, 20, 16);
            var expected = new DateTimeOffset(new DateTime(2018, 7, 1, 14, 20, 16), TimeSpan.FromHours(2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundUpWinterTime_When_TimeSpanIsQuarter()
        {
            var actual = _winterTime.Round(TimeSpan.FromMinutes(15));
            var expected = new DateTimeOffset(2018, 1, 1, 13, 0, 0, TimeSpan.FromHours(1));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundUpWinterTime_When_TimeSpanIsHour()
        {
            var actual = _winterTime.Round(TimeSpan.FromHours(1));
            var expected = new DateTimeOffset(2018, 1, 1, 13, 0, 0, TimeSpan.FromHours(1));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundUpWinterTime_When_TimeSpanIsDay()
        {
            var actual = _winterTime.Round(TimeSpan.FromDays(1));
            var expected = new DateTimeOffset(2018, 1, 2, 0, 0, 0, 0, TimeSpan.FromHours(1));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundDownSummerTime_When_TimeSpanIsQuarter()
        {
            var actual = _summerTime.Round(TimeSpan.FromMinutes(15));
            var expected = new DateTimeOffset(2018, 7, 1, 1, 15, 0, TimeSpan.FromHours(2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundDownSummerTime_When_TimeSpanIsHour()
        {
            var actual = _summerTime.Round(TimeSpan.FromHours(1));
            var expected = new DateTimeOffset(2018, 7, 1, 1, 0, 0, TimeSpan.FromHours(2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void Round_Should_RoundDownSummerTime_When_TimeSpanIsDay()
        {
            var actual = _summerTime.Round(TimeSpan.FromDays(1));
            var expected = new DateTimeOffset(2018, 7, 1, 0, 0, 0, TimeSpan.FromHours(2));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void RoundDown_Should_RoundDownWinterTime_When_TimeSpanIsQuarter()
        {
            var actual = _winterTime.RoundDown(TimeSpan.FromMinutes(15));
            var expected = new DateTimeOffset(2018, 1, 1, 12, 45, 0, TimeSpan.FromHours(1));
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("DateTimeOffsetExtensions")]
        public void RoundUp_Should_RoundDownWinterTime_When_TimeSpanIsQuarter()
        {
            var actual = _summerTime.RoundUp(TimeSpan.FromMinutes(15));
            var expected = new DateTimeOffset(2018, 7, 1, 1, 30, 0, TimeSpan.FromHours(2));
            Assert.Equal(expected, actual);
        }
    }
}