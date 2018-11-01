namespace SWE.BasicType.Date.Test.Utilities
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.BasicType.Date.Utilities;
    using SWE.Xunit.Attributes;
    using System;

    public class ConversionUtilitiesTest
    {
        [Fact]
        [Category("ConversionUtilities")]
        public void UnixTimeStampToDateTimeOffset_Should_ReturnDate_With_NeutralOffset()
        {
            var actual = ConversionUtilities.UnixTimeStampToDateTimeOffset(0, 0);
            actual.Should().Be(new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero));
        }

        [Fact]
        [Category("ConversionUtilities")]
        public void UnixTimeStampToDateTimeOffset_Should_ReturnDate_With_PositiveOffset()
        {
            var actual = ConversionUtilities.UnixTimeStampToDateTimeOffset(60 * 60 * 24, 60 * 60);
            actual.Should().Be(new DateTimeOffset(1970, 1, 2, 1, 0, 0, TimeSpan.FromHours(1)));
        }

        [Fact]
        [Category("ConversionUtilities")]
        public void UnixTimeStampToDateTimeOffset_Should_ReturnDate_With_NegativeOffset()
        {
            var actual = ConversionUtilities.UnixTimeStampToDateTimeOffset(60 * 60 * 24 * 33, -60 * 30 * 5);
            actual.Should().Be(new DateTimeOffset(1970, 2, 2, 21, 30, 0, TimeSpan.FromMinutes(-150)));
        }

        [Fact]
        [Category("ConversionUtilities")]
        public void DateTimeOffsetToUnixTimeStamp_Should_ReturnUnixTimeStamp()
        {
            var referenceDate = new DateTimeOffset(1970, 2, 2, 21, 30, 0, TimeSpan.FromMinutes(-150));
            var actual = ConversionUtilities.DateTimeOffsetToUnixTimeStamp(referenceDate);
            actual.Should().Be(60 * 60 * 24 * 33);
        }
    }
}