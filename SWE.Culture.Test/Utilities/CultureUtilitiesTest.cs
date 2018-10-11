using System;

namespace SWE.Culture.Test.Utilities
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Culture.Utilities;
    using SWE.Xunit.Attributes;
    using System;

    public class CultureUtilitiesTest
    {
        [Theory]
        [Category("CultureUtilities")]
        [InlineData(" a- ble", "a-ble")]
        [InlineData(" ab -cd ", "ab-cd")]
        [InlineData(" n l _ nl", "nl-nl")]
        [InlineData(" nl", "nl")]
        public void CleanCutureCodeTest(string value, string expected)
        {
            var actual = CultureUtilities.CleanCutureCode(value);
            actual.Should().Be(expected);
        }

        [Theory]
        [Category("CultureUtilities")]
        [InlineData(" ")]
        [InlineData(" nld-be ")]
        [InlineData("n-BL")]
        [InlineData("dfj-")]
        public void CleanCutureCodeThrowsTest(string value)
        {
            Action action = () => CultureUtilities.CleanCutureCode(value);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Category("CultureUtilities")]
        [InlineData("nlNLs", "nlNLs", "")]
        [InlineData("NL_nl", "nl", "NL")]
        [InlineData("nl-NL", "nl", "NL")]
        public void SplitCultureCodePartsTest(string value, string expectedLanguageCode, string expectedLocaleCode)
        {
            var (languageCode, localeCode) = CultureUtilities.SplitCultureCodeParts(value);
            languageCode.Should().Be(expectedLanguageCode);
            localeCode.Should().Be(expectedLocaleCode);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("nl", "", "nl")]
        [InlineData("nl", "nl", "nl-NL")]
        [InlineData("", "nl", "-NL")]
        public void ConcatCultureCodePartsTest(string languageCode, string localeCode, string expected)
        {
            CultureUtilities.ConcatCultureCodeParts(languageCode, localeCode).Should().Be(expected);
        }
    }
}