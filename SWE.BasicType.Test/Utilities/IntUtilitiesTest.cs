namespace SWE.BasicType.Test.Utilities
{
    using global::Xunit;

    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;

    public class IntUtilitiesTest
    {
        [Theory]
        [InlineData(1234, 234, 124)]
        [InlineData(0, 234, 124)]
        [InlineData(1, 1, 0)]
        [InlineData(12, -12, -9)]
        [InlineData(12, 12, 9)]
        [Category("IntUtilities")]
        public void IsDefault_Should_ReturnFalse(int factor, int denominator, int remainder)
        {
            var value = (factor * denominator) + remainder;
            var actualFactor = IntUtilities.TryIntDivision(value, denominator, out var _actualRemainder);

            Assert.Equal(factor, actualFactor);
            Assert.Equal(remainder, _actualRemainder);
        }
    }
}