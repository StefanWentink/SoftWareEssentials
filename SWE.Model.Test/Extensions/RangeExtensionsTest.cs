namespace SWE.Model.Test.Extensions
{
    using global::Xunit;

    using SWE.Model.Extensions;
    using SWE.Model.Interfaces;
    using SWE.Model.Models;
    using SWE.Model.Test.Data;
    using SWE.Xunit.Attributes;
    using System.Linq;

    public class RangeExtensionsTest
    {
        private const int DefaultFrom = -1;

        private const int DefaultTill = 11;

        private readonly IRange<int> _range = new RangeModel<int>(DefaultFrom, DefaultTill);

        private readonly RangeAdapter<WithStub, int> _rangeAdapter =
            new RangeAdapter<WithStub, int>(StubFactory.GetWithStub(), x => x.IntProperty, x => x.SecondIntProperty);

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-2, false)]
        [InlineData(-1, true)]
        [InlineData(0, true)]
        [InlineData(10, true)]
        [InlineData(11, false)]
        [InlineData(12, false)]
        public void InRange(int value, bool expected)
        {
            Assert.Equal(expected, _range.InRange(value));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-2, false)]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(10, true)]
        [InlineData(11, false)]
        [InlineData(12, false)]
        public void Between(int value, bool expected)
        {
            Assert.Equal(expected, _range.InBetween(value));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, true)]      // in range - overlapping boundary 
        [InlineData(-2, 11, true)]      // in range - fully overlapping - on boundary
        [InlineData(-2, 12, true)]      // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, true)]      // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, true)]      // in range - fully overlapping - on boundary
        [InlineData(10, 11, true)]      // in range - partial overlap
        [InlineData(10, 12, true)]      // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range

        [InlineData(12, 13, false)]
        public void OverLaps(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.OverLaps(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, false)]     // in range - overlapping boundary 
        [InlineData(-2, 11, false)]     // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, true)]      // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, true)]      // in range - fully overlapping - on boundary
        [InlineData(10, 11, false)]     // in range - partial overlap
        [InlineData(10, 12, false)]     // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void FromEquals(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.FromEquals(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, false)]     // in range - overlapping boundary 
        [InlineData(-2, 11, true)]      // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, false)]     // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, false)]     // in range - fully overlapping - on boundary
        [InlineData(10, 11, true)]      // in range - partial overlap
        [InlineData(10, 12, false)]     // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void TillEquals(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.TillEquals(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, false)]     // in range - overlapping boundary 
        [InlineData(-2, 11, false)]     // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, false)]     // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, false)]     // in range - fully overlapping - on boundary
        [InlineData(10, 11, false)]     // in range - partial overlap
        [InlineData(10, 12, false)]     // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void RangeEquals(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.RangeEquals(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, false)]     // in range - overlapping boundary 
        [InlineData(-2, 11, false)]     // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, true)]      // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, false)]     // in range - fully overlapping - on boundary
        [InlineData(10, 11, true)]      // in range - partial overlap
        [InlineData(10, 12, false)]     // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void Ecapsulates(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.Ecapsulates(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, false)]     // in range - overlapping boundary 
        [InlineData(-2, 11, false)]     // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, true)]      // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, true)]      // in range - fully overlapping - on boundary
        [InlineData(10, 11, true)]      // in range - partial overlap
        [InlineData(10, 12, true)]      // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void EcapsulatesFrom(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.EcapsulatesFrom(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(-3, -2, false)]     // out of range
        [InlineData(-2, -1, false)]     // out of range - on boundary
        [InlineData(-2, 10, true)]      // in range - overlapping boundary 
        [InlineData(-2, 11, true)]      // in range - fully overlapping - on boundary
        [InlineData(-2, 12, false)]     // in range - fully overlapping - both boundaries
        [InlineData(-1, 10, true)]      // in range - partial overlap 
        [InlineData(-1, 11, true)]      // in range - exactly overlapping
        [InlineData(-1, 12, false)]     // in range - fully overlapping - on boundary
        [InlineData(10, 11, true)]      // in range - partial overlap
        [InlineData(10, 12, false)]     // in range - overlapping boundary
        [InlineData(11, 12, false)]     // out of range - on boundary
        [InlineData(11, 13, false)]     // out of range
        public void EcapsulatesTill(int from, int till, bool expected)
        {
            Assert.Equal(expected, _range.EcapsulatesTill(new RangeModel<int>(from, till)));
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(21, 22)]
        [InlineData(22, 23)]
        [InlineData(50, 51)]
        [InlineData(51, 52)]
        public void Subtract_Should_ReturnOriginalRange_When_NoOverlap(int from, int till)
        {
            var actual = _rangeAdapter.Subtract(new RangeModel<int>(from, till)).ToList();
            Assert.Equal(StubFactory.IntPropertyValue, _rangeAdapter.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, _rangeAdapter.Till);

            Assert.Single(actual);
            var actualSingle = actual.Single();
            Assert.Equal(StubFactory.IntPropertyValue, actualSingle.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, actualSingle.Till);
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(22, 24)]
        [InlineData(23, 25)]
        public void Subtract_Should_ReturnShortenedRange_When_PartiallyOverlappingFrom(int from, int till)
        {
            var actual = _rangeAdapter.Subtract(new RangeModel<int>(from, till)).ToList();
            Assert.Equal(StubFactory.IntPropertyValue, _rangeAdapter.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, _rangeAdapter.Till);

            Assert.Single(actual);
            var actualSingle = actual.Single();
            Assert.Equal(till, actualSingle.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, actualSingle.Till);
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(48, 50)]
        [InlineData(49, 51)]
        public void Subtract_Should_ReturnShortenedRange_When_PartiallyOverlappingTill(int from, int till)
        {
            var actual = _rangeAdapter.Subtract(new RangeModel<int>(from, till)).ToList();
            Assert.Equal(StubFactory.IntPropertyValue, _rangeAdapter.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, _rangeAdapter.Till);

            Assert.Single(actual);
            var actualSingle = actual.Single();
            Assert.Equal(StubFactory.IntPropertyValue, actualSingle.From);
            Assert.Equal(from, actualSingle.Till);
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(22, 50)]
        [InlineData(22, 51)]
        [InlineData(23, 50)]
        [InlineData(23, 51)]
        public void Subtract_Should_ReturnEmptyResult_When_CompareEncapsulatesRange(int from, int till)
        {
            var actual = _rangeAdapter.Subtract(new RangeModel<int>(from, till)).ToList();
            Assert.Equal(StubFactory.IntPropertyValue, _rangeAdapter.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, _rangeAdapter.Till);

            Assert.Empty(actual);
        }

        [Theory]
        [Category("RangeExtensions")]
        [InlineData(24, 49)]
        [InlineData(24, 25)]
        [InlineData(48, 49)]
        public void Subtract_Should_SplitRange_When_RangeEncapsulatesCompare(int from, int till)
        {
            var actual = _rangeAdapter.Subtract(new RangeModel<int>(from, till)).ToList();
            Assert.Equal(StubFactory.IntPropertyValue, _rangeAdapter.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, _rangeAdapter.Till);

            Assert.Equal(2, actual.Count);
            var actualFirst = actual.OrderBy(x => x.From).First();
            var actualLast = actual.OrderBy(x => x.From).Last();

            Assert.Equal(StubFactory.IntPropertyValue, actualFirst.From);
            Assert.Equal(from, actualFirst.Till);

            Assert.Equal(till, actualLast.From);
            Assert.Equal(StubFactory.SecondIntPropertyValue, actualLast.Till);
        }
    }
}
