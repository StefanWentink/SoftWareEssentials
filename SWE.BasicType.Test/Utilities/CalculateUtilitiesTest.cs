namespace SWE.BasicType.Test.Utilities
{
    using global::Xunit;
    using SWE.BasicType.Test.Data.Models;
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;
    using System;

    public class CalculateUtilitiesTest
    {
        [Fact]
        [Category("CalculateUtilities")]
        public void TrySubtract_Should_ReturnFalse_When_NotSubtractableBase()
        {
            const string baseValue = "Stefan";
            const int value = 12;
            Assert.False(CalculateUtilities.TrySubtract(baseValue, value, out _));
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TrySubtract_Should_ReturnFalse_When_NotSubtractableValue()
        {
            const int baseValue = 12;
            var value = TimeSpan.FromHours(1);
            Assert.False(CalculateUtilities.TrySubtract(baseValue, value, out _));
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryAdd_Should_ReturnFalse_When_NotSubtractableBase()
        {
            const int baseValue = 12;
            const double value = 1;
            Assert.False(CalculateUtilities.TryAdd(baseValue, value, out _));
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryAdd_Should_ReturnFalse_When_NotSubtractableValue()
        {
            const double baseValue = 12;
            var value = new StructStub();
            Assert.False(CalculateUtilities.TryAdd(baseValue, value, out _));
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TrySubtract_Should_ReturnTrue()
        {
            const double baseValue = 12;
            const int value = 1;
            Assert.True(CalculateUtilities.TrySubtract(baseValue, value, out var _result));
            Assert.Equal(11, _result);
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryAdd_Should_ReturnTrue()
        {
            var baseValue = new DateTime(2018, 1, 1, 12, 30, 45).ToLocalTime();
            var value = TimeSpan.FromDays(1);
            Assert.True(CalculateUtilities.TryAdd(baseValue, value, out var _result));
            Assert.Equal(new DateTime(2018, 1, 2, 12, 30, 45).ToLocalTime(), _result);
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryInvert_Should_ReturnTrue_When_Int()
        {
            const int value = 3;
            Assert.True(CalculateUtilities.TryInvert(value, out var _result));
            Assert.Equal(value * -1, _result);
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryInvert_Should_ReturnTrue_When_Double()
        {
            const double value = 3.2;
            Assert.True(CalculateUtilities.TryInvert(value, out var _result));
            Assert.True(CompareUtilities.EqualsWithinTolerance(value * -1, _result, 6));
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryInvert_Should_ReturnTrue_When_TimeSpan()
        {
            var value = TimeSpan.FromHours(3);
            Assert.True(CalculateUtilities.TryInvert(value, out var _result));
            Assert.Equal(value * -1, _result);
        }

        [Fact]
        [Category("CalculateUtilities")]
        public void TryInvert_Should_ReturnFalse_When_String()
        {
            const string value = "String";
            Assert.False(CalculateUtilities.TryInvert(value, out _));
        }
    }
}