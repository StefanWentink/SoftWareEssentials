namespace SWE.BasicType.Date.Test.Utilities
{
    using global::Xunit;
    using SWE.BasicType.Date.Utilities;
    using SWE.Xunit.Attributes;
    using System;
    using System.Linq;

    public class TimeZoneInfoUtilitiesTest
    {
        [Theory]
        [InlineData("NL")]
        [InlineData("DE")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZoneInfoByCountryCode_Should_ReturnResult(string countryCode)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZoneInfoByCountryCode(countryCode);
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("AU")]
        [InlineData("US")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZoneInfoByCountryCode_Should_ThrowArgumentException_When_MultipleResults(string countryCode)
        {
            Assert.Throws<ArgumentException>(() => TimeZoneInfoUtilities.GetTimeZoneInfoByCountryCode(countryCode));
        }

        [Theory]
        [InlineData("")]
        [InlineData("ZZ")]
        [InlineData("NLD")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZoneInfoByCountryCode_Should_ThrowArgumentException_When_NoResults(string countryCode)
        {
            Assert.Throws<ArgumentException>(() => TimeZoneInfoUtilities.GetTimeZoneInfoByCountryCode(countryCode));
        }

        [Theory]
        [InlineData("NL")]
        [InlineData("DE")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZoneInfosByCountryCode_ShouldReturnSingle(string countryCode)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZoneInfosByCountryCode(countryCode);
            Assert.Single(actual);
        }

        [Theory]
        [InlineData("AU", 9)]
        [InlineData("US", 9)]
        [InlineData("BR", 8)]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZoneInfosByCountryCode_ShouldReturnMultiple(string countryCode, int expected)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZoneInfosByCountryCode(countryCode);
            Assert.Equal(expected, actual.Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData("ZZ")]
        [InlineData("NLD")]
        [Category("TimeZoneInfoUtilities")]
        public void GetMapZonesByCountryCode_Should_ReturnEmpty(string countryCode)
        {
            var actual = TimeZoneInfoUtilities.GetMapZonesByCountryCode(countryCode);
            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("NL")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZonesByCountryCode_ShouldReturnSingle(string countryCode)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZonesByCountryCode(countryCode);
            Assert.Single(actual);
        }

        [Theory]
        [InlineData("AU", 13)]
        [InlineData("BR", 16)]
        [InlineData("US", 29)]
        [InlineData("DE", 2)]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZonesByCountryCode_ShouldReturnMultiple(string countryCode, int expected)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZonesByCountryCode(countryCode);
            Assert.Equal(expected, actual.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ZZ")]
        [InlineData("NLD")]
        [Category("TimeZoneInfoUtilities")]
        public void GetTimeZonesByCountryCode_Should_ReturnEmpty(string countryCode)
        {
            var actual = TimeZoneInfoUtilities.GetTimeZonesByCountryCode(countryCode);
            Assert.Empty(actual);
        }
    }
}