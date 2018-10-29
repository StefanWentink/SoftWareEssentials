namespace SWE.AnomalyAnalysis.Test.Models
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;
    using FluentAssertions;
    using System;

    public class HistorySettingsTest
    {
        [Theory]
        [Category("HistorySettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        public void Constructor_Should_CreateHistorySettings_WithPreserveCount(long value)
        {
            var actual = new HistorySettings(value);
            actual.PreserveCount.Should().Be(value);
            actual.PreserveTimeSpan.Should().Be(TimeSpan.Zero);
        }

        [Theory]
        [Category("HistorySettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        public void Constructor_Should_CreateHistorySettings_WithPreserveTimeSpan(int value)
        {
            var timeSpan = TimeSpan.FromHours(value);
            var actual = new HistorySettings(timeSpan);
            actual.PreserveCount.Should().Be(0);
            actual.PreserveTimeSpan.Should().Be(timeSpan);
        }

        [Fact]
        [Category("HistorySettingsExtensions")]
        public void Constructor_Should_ThrowException_When_CountLessThanZero()
        {
            Action action = () => new HistorySettings(-1);
            action.Should().Throw<ArgumentException>();
        }
    }
}