namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using FluentAssertions;
    using System;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;

    public class ValueRangeTest
    {
        [Theory]
        [Category("ValueRange")]
        [InlineData(1, 2, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(1, 2, 2)]
        [InlineData(0, 0, 0)]
        public void Constructor_Should_CreateValidModel_With_Double(
            double low, double normal, double high)
        {
            var actual = new ValueRange<double>(low, normal, high);

            actual.Low.Should().Be(low);
            actual.Normal.Should().Be(normal);
            actual.High.Should().Be(high);
        }

        [Theory]
        [Category("ValueRange")]
        [InlineData(1, 2, 3)]
        [InlineData(2, 2, 3)]
        [InlineData(1, 2, 2)]
        [InlineData(0, 0, 0)]
        public void Constructor_Should_CreateValidModel_With_DateTime(
            int low, int normal, int high)
        {
            var referenceDate = DateTimeOffset.Now;

            var lowDate = referenceDate.AddTicks(low);
            var normalDate = referenceDate.AddTicks(normal);
            var highDate = referenceDate.AddTicks(high);

            var actual = new ValueRange<DateTimeOffset>(lowDate, normalDate, highDate);

            actual.Low.Should().Be(lowDate);
            actual.Normal.Should().Be(normalDate);
            actual.High.Should().Be(highDate);
        }

        [Theory]
        [Category("ValueRange")]
        [InlineData(2.1, 2, 3)]
        public void Constructor_Should_ThrowArgumentExceptionLow_With_Double(
            double low, double normal, double high)
        {
            Action action = () => new ValueRange<double>(low, normal, high);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Category("ValueRange")]
        [InlineData(1, 2, 1.9)]
        public void Constructor_Should_ThrowArgumentExceptionHigh_With_Double(
            double low, double normal, double high)
        {
            Action action = () => new ValueRange<double>(low, normal, high);
            action.Should().Throw<ArgumentException>();
        }
    }
}