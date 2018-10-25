namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Enums;
    using SWE.AnomalyAnalysis.EventArgs;
    using SWE.AnomalyAnalysis.Interfaces;
    using SWE.AnomalyAnalysis.Extensions;
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using SWE.BasicType.Utilities;
    using System.Threading;

    public class ValueContainer<TValue> : IValueContainer<TValue>
        where TValue : IComparable<TValue>
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private IDetectionRange<TValue> Range { get; set; }

        private ICalculator<TValue> Calculator { get; }

        private ITriggerSettings TriggerSettings { get; }

        private ConcurrentDictionary<DateTimeOffset, TValue> Values { get; } = new ConcurrentDictionary<DateTimeOffset, TValue>();

        private ConcurrentBag<TValue> Anomalies { get; } = new ConcurrentBag<TValue>();

        private DateTimeOffset NextCalculationDate { get; set; }

        private void SetCalculationDate(DateTimeOffset calculationDate)
        {
            NextCalculationDate = calculationDate.Add(TriggerSettings.TimeSpanTrigger);
        }

        public ValueContainer(
            ICalculator<TValue> calculator,
            ITriggerSettings triggerSettings)
            : this(calculator, triggerSettings, null)
        {
        }

        public ValueContainer(
            ICalculator<TValue> calculator,
            ITriggerSettings triggerSettings,
            IDetectionRange<TValue> range)
        {
            Calculator = calculator;
            TriggerSettings = triggerSettings;
            Range = range;
            SetCalculationDate(DateTimeOffset.Now);
        }

        public async Task AddAsync(TValue value)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                var addResult = await AddValue(value).ConfigureAwait(false);

                if (addResult.anomaly != Anomaly.None)
                {
                    OnAnomalyEvent(value, addResult.anomaly);
                }

                if (addResult.trigger != Trigger.None)
                {
                    var calculationResult = Calculator.Calculate(Values.Values, Anomalies);

                    if (!CompareUtilities.Equals(calculationResult.Normal, Range.Normal))
                    {
                        OnValueChangedEvent(calculationResult.Normal, addResult.trigger);
                    }

                    Range = calculationResult;
                    SetCalculationDate(DateTimeOffset.Now);
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return;
        }

        internal async Task<(Anomaly anomaly, Trigger trigger)> AddValue(TValue value)
        {
            var anomaly = Range.Validate(value);

            if (anomaly == Anomaly.None)
            {
                var referenceDate = await AddValueOnReferenceDate(value).ConfigureAwait(false);
                return (anomaly, TriggerSettings.DetermineTrigger(referenceDate, NextCalculationDate, Values.Count));
            }

            return (anomaly, TriggerSettings.DetermineAnomalyTrigger(Anomalies.Count));
        }

        internal async Task<DateTimeOffset> AddValueOnReferenceDate(TValue value)
        {
            var referenceDate = DateTimeOffset.Now;
            await Task.Run(() =>
            {
                while (!Values.TryAdd(referenceDate, value))
                {
                    referenceDate = DateTimeOffset.Now;
                }
            });

            return referenceDate;
        }

        internal void AddAnomaly(TValue value)
        {
            Anomalies.Add(value);
        }

        public event EventHandler<ValueChangedEventArgs<TValue>> ValueChangedEvent;

        public void OnValueChangedEvent(TValue value, Trigger trigger)
        {
            ValueChangedEvent?.Invoke(this, new ValueChangedEventArgs<TValue>(value, trigger));
        }

        public event EventHandler<AnomalyEventArgs<TValue>> AnomalyEvent;

        public void OnAnomalyEvent(TValue value, Anomaly anomaly)
        {
            AnomalyEvent?.Invoke(this, new AnomalyEventArgs<TValue>(value, anomaly));
        }
    }
}