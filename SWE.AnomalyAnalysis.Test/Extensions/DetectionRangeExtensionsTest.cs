namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;
    using SWE.AnomalyAnalysis.Extensions;
    using FluentAssertions;
    using SWE.AnomalyAnalysis.Enums;

    public class DetectionRangeExtensionsTest
    {
        private DetectionRange<double> Range = new DetectionRange<double>(1, 2, 3, 4, 5);

        [Theory]
        [Category("DetectionRangeExtensions")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validate_Should_ReturnAnomalyNone(double value)
        {
            Range.Validate(value).Should().Be(Anomaly.None);
        }

        [Fact]
        [Category("DetectionRangeExtensions")]
        public void Validate_Should_ReturnAnomalyLow()
        {
            Range.Validate(0.99).Should().Be(Anomaly.Low);
        }

        [Fact]
        [Category("DetectionRangeExtensions")]
        public void Validate_Should_ReturnAnomalyHigh()
        {
            Range.Validate(5.01).Should().Be(Anomaly.High);
        }
    }
}