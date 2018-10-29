namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using FluentAssertions;
    using System;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;

    public class DetectionRangeTest
    {
        [Theory]
        [Category("DetectionRange")]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(1, 1, 3, 4, 5)]
        [InlineData(1, 3, 3, 4, 5)]
        [InlineData(1, 2, 3, 3, 5)]
        [InlineData(1, 2, 3, 5, 5)]
        [InlineData(0, 0, 0, 0, 0)]
        public void Constructor_Should_CreateValidModel_With_Double(
            double maxDetectionLow, double low, double normal, double high, double maxDetectionHigh)
        {
            var actual = new DetectionRange<double>(maxDetectionLow, low, normal, high, maxDetectionHigh);

            actual.MaxDetectionLow.Should().Be(maxDetectionLow);
            actual.Low.Should().Be(low);
            actual.Normal.Should().Be(normal);
            actual.High.Should().Be(high);
            actual.MaxDetectionHigh.Should().Be(maxDetectionHigh);
        }

        [Theory]
        [Category("DetectionRange")]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(1, 1, 3, 4, 5)]
        [InlineData(1, 3, 3, 4, 5)]
        [InlineData(1, 2, 3, 3, 5)]
        [InlineData(1, 2, 3, 5, 5)]
        [InlineData(0, 0, 0, 0, 0)]
        public void Constructor_Should_CreateValidModel_With_DateTime(
            int maxDetectionLow, int low, int normal, int high, int maxDetectionHigh)
        {
            var referenceDate = DateTimeOffset.Now;

            var maxDetectionLowDate = referenceDate.AddTicks(maxDetectionLow);
            var lowDate = referenceDate.AddTicks(low);
            var normalDate = referenceDate.AddTicks(normal);
            var highDate = referenceDate.AddTicks(high);
            var maxDetectionHighDate = referenceDate.AddTicks(maxDetectionHigh);

            var actual = new DetectionRange<DateTimeOffset>(maxDetectionLowDate, lowDate, normalDate, highDate, maxDetectionHighDate);

            actual.MaxDetectionLow.Should().Be(maxDetectionLowDate);
            actual.Low.Should().Be(lowDate);
            actual.Normal.Should().Be(normalDate);
            actual.High.Should().Be(highDate);
            actual.MaxDetectionHigh.Should().Be(maxDetectionHighDate);
        }

        [Theory]
        [Category("DetectionRange")]
        [InlineData(2.1, 2, 3, 4, 5)]
        public void Constructor_Should_ThrowArgumentExceptionOnDetectionLow_With_Double(
            double maxDetectionLow, double low, double normal, double high, double maxDetectionHigh)
        {
            Action action = () => new DetectionRange<double>(maxDetectionLow, low, normal, high, maxDetectionHigh);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Category("DetectionRange")]
        [InlineData(1, 3.1, 3, 4, 5)]
        public void Constructor_Should_ThrowArgumentExceptionLow_With_Double(
            double maxDetectionLow, double low, double normal, double high, double maxDetectionHigh)
        {
            Action action = () => new DetectionRange<double>(maxDetectionLow, low, normal, high, maxDetectionHigh);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Category("DetectionRange")]
        [InlineData(1, 2, 3, 2.9, 5)]
        public void Constructor_Should_ThrowArgumentExceptionOnDetectionHigh_With_Double(
            double maxDetectionLow, double low, double normal, double high, double maxDetectionHigh)
        {
            Action action = () => new DetectionRange<double>(maxDetectionLow, low, normal, high, maxDetectionHigh);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [Category("DetectionRange")]
        [InlineData(1, 2, 3, 4, 3.9)]
        public void Constructor_Should_ThrowArgumentExceptionHigh_With_Double(
            double maxDetectionLow, double low, double normal, double high, double maxDetectionHigh)
        {
            Action action = () => new DetectionRange<double>(maxDetectionLow, low, normal, high, maxDetectionHigh);
            action.Should().Throw<ArgumentException>();
        }
    }
}