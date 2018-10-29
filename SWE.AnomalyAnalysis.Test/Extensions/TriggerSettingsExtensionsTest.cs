namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;
    using SWE.AnomalyAnalysis.Extensions;
    using FluentAssertions;
    using SWE.AnomalyAnalysis.Enums;
    using System;

    public class TriggerSettingsExtensionsTest
    {
        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        public void DetermineAnomalyTrigger_Should_ReturnTriggerNone(long value)
        {
            var trigger = new TriggerSettings(2);
            trigger.DetermineAnomalyTrigger(value).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DetermineAnomalyTrigger_Should_ReturnTriggerNone_When_TriggerNotRequested(long value)
        {
            var trigger = new TriggerSettings(TimeSpan.FromSeconds(1));
            trigger.DetermineAnomalyTrigger(value).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(2)]
        [InlineData(3)]
        public void DetermineAnomalyTrigger_Should_ReturnTriggerAnomaly(long value)
        {
            var trigger = new TriggerSettings(2);
            trigger.DetermineAnomalyTrigger(value).Should().Be(Trigger.Anomaly);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(1)]
        [InlineData(2)]
        public void DetermineTrigger_Should_ReturnTriggerNone_With_TimeSpan(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(value);
            var trigger = new TriggerSettings(TimeSpan.FromSeconds(1));
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, 1000).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void DetermineTrigger_Should_ReturnTriggerNone_When_TriggerNotRequested_With_TimeSpan(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(value);
            var trigger = new TriggerSettings(1);
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, 1000).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(-1)]
        [InlineData(0)]
        public void DetermineTrigger_Should_ReturnTriggerTime_With_TimeSpan(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(value);
            var trigger = new TriggerSettings(TimeSpan.FromSeconds(1));
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, 1000).Should().Be(Trigger.Time);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        public void DetermineTrigger_Should_ReturnTriggerNone_With_Count(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(-1);
            var trigger = new TriggerSettings(3, 2);
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, value).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DetermineTrigger_Should_ReturnTriggerNone_When_TriggerNotRequested_With_Count(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(-1);
            var trigger = new TriggerSettings(1);
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, value).Should().Be(Trigger.None);
        }

        [Theory]
        [Category("TriggerSettingsExtensions")]
        [InlineData(2)]
        [InlineData(3)]
        public void DetermineTrigger_Should_ReturnTriggerCount_With_Count(long value)
        {
            var referenceDate = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var nextCalculationDate = referenceDate.AddSeconds(-1);
            var trigger = new TriggerSettings(3, 2);
            trigger.DetermineTrigger(referenceDate, nextCalculationDate, value).Should().Be(Trigger.Count);
        }
    }
}