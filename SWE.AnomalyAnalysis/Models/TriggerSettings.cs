namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using System;

    /// <inheritdoc></inheritdoc>
    public class TriggerSettings : ITriggerSettings
    {
        /// <inheritdoc></inheritdoc>
        public long AnomalyCountTrigger { get; } = 0;

        /// <inheritdoc></inheritdoc>
        public long CountTrigger { get; } = 0;

        /// <inheritdoc></inheritdoc>
        public long InitialCountTrigger { get; } = 0;

        /// <inheritdoc></inheritdoc>
        public TimeSpan TimeSpanTrigger { get; } = TimeSpan.Zero;

        public bool ApplyInitialCountTrigger { get; }

        public bool ApplyCountTrigger { get; }

        public bool ApplyAnomalyCountTrigger { get; }

        public bool ApplyTimeSpanTrigger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="anomalyCount"/> is not applicable.</exception>
        public TriggerSettings(
           long anomalyCount)
            : this(TimeSpan.Zero, 0, 0, anomalyCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="countTrigger"></param>
        /// <param name="initialCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="countTrigger"/> is not applicable.</exception>
        public TriggerSettings(
           long countTrigger,
           long initialCountTrigger)
            : this(TimeSpan.Zero, countTrigger, initialCountTrigger, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="countTrigger"></param>
        /// <param name="initialCountTrigger"></param>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="countTrigger"/> and <see cref="anomalyCount"/> are not applicable.</exception>
        public TriggerSettings(
           long countTrigger,
           long initialCountTrigger,
           long anomalyCount)
            : this(TimeSpan.Zero, countTrigger, initialCountTrigger, anomalyCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="timeSpanTrigger"></param>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/> is not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger)
            : this(timeSpanTrigger, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="timeSpanTrigger"></param>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/>, and <see cref="anomalyCount"/> are not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger,
            long anomalyCount)
            : this(timeSpanTrigger, 0, 0, anomalyCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="timeSpanTrigger"></param>
        /// <param name="countTrigger"></param>
        /// <param name="initialCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/>, and <see cref="countTrigger"/> are not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger,
            long countTrigger,
            long initialCountTrigger)
            : this(timeSpanTrigger, countTrigger, initialCountTrigger, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="timeSpanTrigger"></param>
        /// <param name="countTrigger"></param>
        /// <param name="initialCountTrigger"></param>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/>, <see cref="countTrigger"/> and <see cref="anomalyCount"/> are not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger,
            long countTrigger,
            long initialCountTrigger,
            long anomalyCount)
        {
            TimeSpanTrigger = timeSpanTrigger;
            CountTrigger = countTrigger;
            InitialCountTrigger = initialCountTrigger;
            AnomalyCountTrigger = anomalyCount;

            ApplyTimeSpanTrigger = timeSpanTrigger.TotalSeconds > 0;
            ApplyCountTrigger = countTrigger > 0;
            ApplyInitialCountTrigger = initialCountTrigger > 0;
            ApplyAnomalyCountTrigger = anomalyCount > 0;

            if (!(ApplyAnomalyCountTrigger || ApplyCountTrigger || ApplyTimeSpanTrigger))
            {
                throw new ArgumentException($"Either {TimeSpanTrigger}, {CountTrigger} or {AnomalyCountTrigger} must be applied.");
            }
        }
    }
}