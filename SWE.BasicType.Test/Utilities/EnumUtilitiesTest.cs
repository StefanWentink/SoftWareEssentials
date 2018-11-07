namespace SWE.BasicType.Test.Utilities
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.BasicType.Test.Data.Enums;
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;
    using System.Linq;

    public class EnumUtilitiesTest
    {
        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnEmpty_WhenEmpty()
        {
            Assert.Empty(EnumUtilities.GetValues<Empty>());
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnEmpty_WhenEmpty_With_Expression()
        {
            Assert.Empty(EnumUtilities.GetValues<Empty>(x => x.ToString().Contains("Value")));
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnTwo()
        {
            Assert.Equal(4, EnumUtilities.GetValues<NonEmpty>().Count());
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnOne_With_Expression()
        {
            Assert.Single(EnumUtilities.GetValues<NonEmpty>(x => x.ToString().Contains(nameof(NonEmpty.FirstValue))));
        }

        [Theory]
        [Category("EnumUtilities")]
        [InlineData(NonEmpty.FirstValue, false, nameof(NonEmpty.FirstValue))]
        [InlineData(NonEmpty.FirstValue, true, "one")]
        [InlineData(NonEmpty.SecondValue, false, nameof(NonEmpty.SecondValue))]
        [InlineData(NonEmpty.SecondValue, true, nameof(NonEmpty.SecondValue))]
        [InlineData(NonEmpty.ThirdValue, false, nameof(NonEmpty.ThirdValue))]
        [InlineData(NonEmpty.ThirdValue, true, "Three")]
        public void GetDescription_Should_ReturnOne_With_Expression(
            NonEmpty value,
            bool readAttribute,
            string expected)
        {
            var actual = value.GetDescription(readAttribute);
            actual.Should().Be(expected);
        }

        [Theory]
        [Category("EnumUtilities")]
        [InlineData("one", NonEmpty.FirstValue)]
        [InlineData("Three", NonEmpty.ThirdValue)]
        public void ParseEnum_Should_ReturnEnum(
            string value,
            NonEmpty expected)
        {
            var actual = EnumUtilities.ParseEnum(value, NonEmpty.Unknown);
            actual.Should().Be(expected);
        }

        [Theory]
        [Category("EnumUtilities")]
        [InlineData(nameof(NonEmpty.FirstValue))]
        [InlineData(nameof(NonEmpty.SecondValue))]
        [InlineData(nameof(NonEmpty.ThirdValue))]
        public void ParseEnum_Should_ReturnDefaultValue(
            string value)
        {
            var actual = EnumUtilities.ParseEnum(value, NonEmpty.Unknown);
            actual.Should().Be(NonEmpty.Unknown);
        }
    }
}