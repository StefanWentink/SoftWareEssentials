namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using FluentAssertions;
    using System;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;

    public class TriggerSettingsTest
    {
        [Theory]
        [Category("TriggerSettings")]
        [InlineData(1, 0, 0, 0)]
        [InlineData(1, 2, 0, 0)]
        [InlineData(1, 0, 3, 0)]
        [InlineData(1, 0, 0, 4)]
        [InlineData(1, 2, 3, 0)]
        [InlineData(1, 2, 0, 4)]
        [InlineData(1, 2, 3, 4)]
        [InlineData(0, 2, 0, 0)]
        [InlineData(0, 0, 0, 4)]
        [InlineData(0, 2, 3, 0)]
        [InlineData(0, 2, 0, 4)]
        [InlineData(0, 2, 3, 4)]
        public void Constructor_Should_CreateValidModel(
            int seconds, long countTrigger, long initialCountTrigger, long anomalyCountTrigger)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            var actual = new TriggerSettings(timeSpan, countTrigger, initialCountTrigger, anomalyCountTrigger);

            actual.TimeSpanTrigger.Should().Be(timeSpan);
            actual.CountTrigger.Should().Be(countTrigger);
            actual.InitialCountTrigger.Should().Be(initialCountTrigger);
            actual.AnomalyCountTrigger.Should().Be(anomalyCountTrigger);
        }

        [Theory]
        [Category("TriggerSettings")]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 0, 1, 0)]
        public void Constructor_Should_ThrowArgumentException(
            int seconds, long countTrigger, long initialCountTrigger, long anomalyCountTrigger)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            Action action = () => new TriggerSettings(timeSpan, countTrigger, initialCountTrigger, anomalyCountTrigger);
            action.Should().Throw<ArgumentException>();
        }
    }
}