namespace SWE.BasicType.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.BasicType.Test.Data.Enums;
    using SWE.BasicType.Extensions;
    using FluentAssertions;

    public class EnumExtensionsTest
    {
        [Theory]
        [Category("EnumExtensions")]
        [InlineData(NonEmpty.FirstValue, "one")]
        [InlineData(NonEmpty.SecondValue, nameof(NonEmpty.SecondValue))]
        [InlineData(NonEmpty.ThirdValue, "Three")]
        public void GetEnumDescription_Should_ReturnDEscriptionAttribute_When_Available(NonEmpty value, string expected)
        {
            value.GetEnumDescription().Should().Be(expected);
        }
    }
}