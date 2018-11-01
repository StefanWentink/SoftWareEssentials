namespace SWE.AnomalyAnalysis.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;
    using SWE.AnomalyAnalysis.Extensions;
    using FluentAssertions;
    using System.Collections.Concurrent;
    using System;
    using System.Collections.Generic;
    using SWE.BasicType.Utilities;

    public class HistorySettingsExtensionsTest
    {
        private ConcurrentDictionary<DateTimeOffset, double> GetValues()
        {
            return new ConcurrentDictionary<DateTimeOffset, double>(
                new List<KeyValuePair<DateTimeOffset, double>>
                {
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero), 1.0) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 1, 0, 0, 1, TimeSpan.Zero), 1.1) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 2, 0, 0, 0, TimeSpan.Zero), 2.0) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 2, 0, 0, 1, TimeSpan.Zero), 2.1) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 3, 0, 0, 0, TimeSpan.Zero), 3.0) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 3, 0, 0, 1, TimeSpan.Zero), 3.1) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 4, 0, 0, 0, TimeSpan.Zero), 4.0) },
                    { new KeyValuePair<DateTimeOffset, double>(new DateTimeOffset(2018, 1, 4, 0, 0, 1, TimeSpan.Zero), 4.1) }
                });
        }

        private ConcurrentBag<double> GetAnomalies()
        {
            return new ConcurrentBag<double>(new List<double> { -1, 8 });
        }

        [Theory]
        [Category("HistorySettingsExtensions")]
        [InlineData(long.MaxValue)]
        [InlineData(9)]
        [InlineData(8)]
        [InlineData(7)]
        [InlineData(1)]
        [InlineData(0)]
        public void ReduceValues_Should_ReduceToValue_When_PreserveCount(long preserve)
        {
            var settings = new HistorySettings(preserve);
            var values = GetValues();
            var anomalies = GetAnomalies();
            var count = values.Count;
            var reductionResult = settings.ReduceValues(values, anomalies, new DateTimeOffset(2018, 1, 2, 0, 0, 1, TimeSpan.Zero));

            var expected = CompareUtilities.Min(count, CompareUtilities.Max(0, count - preserve + anomalies.Count));
            reductionResult.Should().Be(expected);
            values.Count.Should().Be(count - (int)expected);
            anomalies.Count.Should().Be(0);
        }

        [Theory]
        [Category("HistorySettingsExtensions")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void ReduceValues_Should_ReduceToValue_When_PreserveTimeSpan(int preserve)
        {
            var settings = new HistorySettings(TimeSpan.FromSeconds(1));
            var values = GetValues();
            var anomalies = GetAnomalies();
            var count = values.Count;
            var referenceDate = new DateTimeOffset(2018, 1, 5, 0, 0, 1, TimeSpan.Zero).AddDays(-preserve);
            var reductionResult = settings.ReduceValues(values, anomalies, referenceDate);

            var expected = CompareUtilities.Min(count, CompareUtilities.Max(0, 2 * (4 - preserve) + anomalies.Count));
            reductionResult.Should().Be(expected);
            values.Count.Should().Be(count - expected);
            anomalies.Count.Should().Be(0);
        }
    }
}