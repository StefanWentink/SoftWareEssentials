namespace SWE.AnomalyAnalysis.Test.Models
{
    using SWE.Xunit.Attributes;
    using FluentAssertions;
    using AnomalyAnalysis.EventArgs;
    using System;
    using global::Xunit;
    using SWE.AnomalyAnalysis.Models;
    using SWE.AnomalyAnalysis.Models.Calculators;
    using SWE.AnomalyAnalysis.Enums;
    using System.Threading.Tasks;

    public class ValueContainerTest
    {
        [Fact]
        [Category("ValueContainer")]
        public async void Container_Should_TriggerOnTimeSpan_With_Count()
        {
            var calculator = new StandardDeviationCalculator(3);
            var trigger = new TriggerSettings(TimeSpan.FromSeconds(1));
            var range = new DetectionRange<double>(300, 800, 1000, 1200, 1700);
            var container = new ValueContainer<double>(calculator, trigger, range);

            var anomalyLowTriggeredCounter = 0;
            var anomalyHighTriggeredCounter = 0;
            var valueChangedTriggeredCounter = 0;
            double value = 0;
            var triggerValue = Trigger.None;

            container.AnomalyEvent += (object _, AnomalyEventArgs<double> e) =>
            {
                if (e.Anomaly == Anomaly.Low)
                {
                    anomalyLowTriggeredCounter++;
                }
                else if (e.Anomaly == Anomaly.High)
                {
                    anomalyHighTriggeredCounter++;
                }
            };

            container.ValueChangedEvent += (object _, ValueChangedEventArgs<double> e) =>
            {
                valueChangedTriggeredCounter++;
                value = e.Value;
                triggerValue = e.Trigger;
            };

            for (var i = 700; i <= 900; i++)
            {
                await container.AddAsync(i).ConfigureAwait(false);
            }

            anomalyLowTriggeredCounter.Should().Be(0);
            anomalyHighTriggeredCounter.Should().Be(0);
            valueChangedTriggeredCounter.Should().Be(0);

            await Task.Delay(1000).ConfigureAwait(false);

            await container.AddAsync(800).ConfigureAwait(false);

            anomalyLowTriggeredCounter.Should().Be(0);
            anomalyHighTriggeredCounter.Should().Be(0);
            valueChangedTriggeredCounter.Should().Be(1);
            value.Should().Be(800);
            triggerValue.Should().Be(Trigger.Time);
        }

        [Fact]
        [Category("ValueContainer")]
        public async void Container_Should_TriggerOnCount_With_Count()
        {
            var calculator = new StandardDeviationCalculator(3);
            var trigger = new TriggerSettings(2, 202);
            var range = new DetectionRange<double>(300, 800, 1000, 1200, 1700);
            var container = new ValueContainer<double>(calculator, trigger, range);

            var anomalyLowTriggeredCounter = 0;
            var anomalyHighTriggeredCounter = 0;
            var valueChangedTriggeredCounter = 0;
            double value = 0;
            var triggerValue = Trigger.None;

            container.AnomalyEvent += (object _, AnomalyEventArgs<double> e) =>
            {
                if (e.Anomaly == Anomaly.Low)
                {
                    anomalyLowTriggeredCounter++;
                }
                else if (e.Anomaly == Anomaly.High)
                {
                    anomalyHighTriggeredCounter++;
                }
            };

            container.ValueChangedEvent += (object _, ValueChangedEventArgs<double> e) =>
            {
                valueChangedTriggeredCounter++;
                value = e.Value;
                triggerValue = e.Trigger;
            };

            for (var i = 700; i <= 900; i++)
            {
                await container.AddAsync(i).ConfigureAwait(false);
            }

            anomalyLowTriggeredCounter.Should().Be(0);
            anomalyHighTriggeredCounter.Should().Be(0);
            valueChangedTriggeredCounter.Should().Be(0);

            await container.AddAsync(800).ConfigureAwait(false);

            anomalyLowTriggeredCounter.Should().Be(0);
            anomalyHighTriggeredCounter.Should().Be(0);
            valueChangedTriggeredCounter.Should().Be(1);
            value.Should().Be(800);
            triggerValue.Should().Be(Trigger.Count);

            await container.AddAsync(800).ConfigureAwait(false);
            await container.AddAsync(900).ConfigureAwait(false);
            valueChangedTriggeredCounter.Should().Be(2);
            value.Should().Be(850);
            triggerValue.Should().Be(Trigger.Count);
        }

        [Fact]
        [Category("ValueContainer")]
        public async void Container_Should_TriggerOnAnomaly_With_Count()
        {
            var calculator = new StandardDeviationCalculator(3);
            var trigger = new TriggerSettings(3);
            var range = new DetectionRange<double>(300, 800, 1000, 1200, 1700);
            var container = new ValueContainer<double>(calculator, trigger, range);

            var anomalyLowTriggeredCounter = 0;
            var anomalyHighTriggeredCounter = 0;
            var valueChangedTriggeredCounter = 0;
            double value = 0;
            var triggerValue = Trigger.None;

            container.AnomalyEvent += (object _, AnomalyEventArgs<double> e) =>
            {
                if (e.Anomaly == Anomaly.Low)
                {
                    anomalyLowTriggeredCounter++;
                }
                else if (e.Anomaly == Anomaly.High)
                {
                    anomalyHighTriggeredCounter++;
                }
            };

            container.ValueChangedEvent += (object _, ValueChangedEventArgs<double> e) =>
            {
                valueChangedTriggeredCounter++;
                value = e.Value;
                triggerValue = e.Trigger;
            };

            for (var i = 700; i <= 900; i++)
            {
                await container.AddAsync(i).ConfigureAwait(false);
            }

            anomalyLowTriggeredCounter.Should().Be(0);
            anomalyHighTriggeredCounter.Should().Be(0);
            valueChangedTriggeredCounter.Should().Be(0);

            await container.AddAsync(200).ConfigureAwait(false);
            await container.AddAsync(1800).ConfigureAwait(false);
            await container.AddAsync(200).ConfigureAwait(false);

            anomalyLowTriggeredCounter.Should().Be(2);
            anomalyHighTriggeredCounter.Should().Be(1);
            valueChangedTriggeredCounter.Should().Be(1);
            value.Should().Be(800);
            triggerValue.Should().Be(Trigger.Anomaly);

            await container.AddAsync(800).ConfigureAwait(false);
            await container.AddAsync(900).ConfigureAwait(false);
            await container.AddAsync(800).ConfigureAwait(false);
            await container.AddAsync(900).ConfigureAwait(false);

            await container.AddAsync(1000).ConfigureAwait(false);
            await container.AddAsync(600).ConfigureAwait(false);
            await container.AddAsync(1000).ConfigureAwait(false);

            anomalyLowTriggeredCounter.Should().Be(3);
            anomalyHighTriggeredCounter.Should().Be(3);
            valueChangedTriggeredCounter.Should().Be(2);
            value.Should().Be(850);
            triggerValue.Should().Be(Trigger.Anomaly);
        }
    }
}