namespace SWE.Model.Test.Extensions
{
    using global::Xunit;
    using SWE.BasicType.Utilities;
    using SWE.BasicType.Test.Data;
    using SWE.Xunit.Attributes;

    public class CompareUtilitiesTest
    {
        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void Equals_When_Int(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.Equals(value, compare));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void Equals_When_DateTime(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.Equals(RangeFactory.ToTestDateTime(value), RangeFactory.ToTestDateTime(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void Equals_When_DateTimeOffset(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.Equals(RangeFactory.ToTestDateTimeOffset(value), RangeFactory.ToTestDateTimeOffset(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void Equals_When_Double(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.Equals(RangeFactory.ToTestDouble(value), RangeFactory.ToTestDouble(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2,          1.9999996, 6, true)]
        [InlineData(2,          1.9999995, 6, true)]
        [InlineData(2,          1.9999994, 6, false)]
        [InlineData(2.0000001,  1.9999996, 6, true)]
        [InlineData(2.0000001,  1.9999995, 6, false)]
        [InlineData(2,          1.9999994, 5, true)]
        public void EqualsWithinTolerance(double value, double compare, int tolerance, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.EqualsWithinTolerance(value, compare, tolerance));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void GreaterOrEqualTo_When_Int(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterOrEqualTo(value, compare));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void GreaterOrEqualTo_When_DateTime(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterOrEqualTo(RangeFactory.ToTestDateTime(value), RangeFactory.ToTestDateTime(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void GreaterOrEqualTo_When_DateTimeOffset(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterOrEqualTo(RangeFactory.ToTestDateTimeOffset(value), RangeFactory.ToTestDateTimeOffset(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, false)]
        public void GreaterOrEqualTo_When_Double(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterOrEqualTo(RangeFactory.ToTestDouble(value), RangeFactory.ToTestDouble(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, false)]
        public void GreaterThan_When_Int(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterThan(value, compare));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, false)]
        public void GreaterThan_When_DateTime(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterThan(RangeFactory.ToTestDateTime(value), RangeFactory.ToTestDateTime(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, false)]
        public void GreaterThan_When_DateTimeOffset(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterThan(RangeFactory.ToTestDateTimeOffset(value), RangeFactory.ToTestDateTimeOffset(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, false)]
        public void GreaterThan_When_Double(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.GreaterThan(RangeFactory.ToTestDouble(value), RangeFactory.ToTestDouble(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        public void SmallerOrEqualTo_When_Int(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerOrEqualTo(value, compare));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        public void SmallerOrEqualTo_When_DateTime(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerOrEqualTo(RangeFactory.ToTestDateTime(value), RangeFactory.ToTestDateTime(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        public void SmallerOrEqualTo_When_DateTimeOffset(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerOrEqualTo(RangeFactory.ToTestDateTimeOffset(value), RangeFactory.ToTestDateTimeOffset(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        public void SmallerOrEqualTo_When_Double(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerOrEqualTo(RangeFactory.ToTestDouble(value), RangeFactory.ToTestDouble(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, true)]
        public void SmallerThan_When_Int(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerThan(value, compare));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, true)]
        public void SmallerThan_When_DateTime(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerThan(RangeFactory.ToTestDateTime(value), RangeFactory.ToTestDateTime(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1, false)]
        [InlineData(2, 2, false)]
        [InlineData(2, 3, true)]
        public void SmallerThan_When_DateTimeOffset(int value, int compare, bool expected)
        {
            Assert.Equal(expected, CompareUtilities.SmallerThan(RangeFactory.ToTestDateTimeOffset(value), RangeFactory.ToTestDateTimeOffset(compare)));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(-1, -2)]
        public void Min_Should_ReturnLowestValue(int high, int low)
        {
            Assert.Equal(low, CompareUtilities.Min(low, high));
        }

        [Theory]
        [Category("CompareUtilities")]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(-2, 1)]
        public void Max_Should_ReturnHighestValue(int low, int high)
        {
            Assert.Equal(high, CompareUtilities.Max(low, high));
        }
    }
}