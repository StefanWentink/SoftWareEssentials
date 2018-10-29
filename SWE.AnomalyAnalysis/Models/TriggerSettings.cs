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

        public bool ApplyInitialCountTrigger { get; private set; }

        public bool ApplyCountTrigger { get; }

        public bool ApplyAnomalyCountTrigger { get; }

        public bool ApplyTimeSpanTrigger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="anomalyCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="anomalyCountTrigger"/> is not applicable.</exception>
        public TriggerSettings(
           long anomalyCountTrigger)
            : this(TimeSpan.Zero, 0, 0, anomalyCountTrigger)
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
        /// <param name="anomalyCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="countTrigger"/> and <see cref="anomalyCountTrigger"/> are not applicable.</exception>
        public TriggerSettings(
           long countTrigger,
           long initialCountTrigger,
           long anomalyCountTrigger)
            : this(TimeSpan.Zero, countTrigger, initialCountTrigger, anomalyCountTrigger)
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
        /// <param name="anomalyCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/>, and <see cref="anomalyCountTrigger"/> are not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger,
            long anomalyCountTrigger)
            : this(timeSpanTrigger, 0, 0, anomalyCountTrigger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerSettings"/> class.
        /// </summary>
        /// <param name="timeSpanTrigger"></param>
        /// <param name="countTrigger"></param>
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
        /// <param name="anomalyCountTrigger"></param>
        /// <exception cref="ArgumentException">If <see cref="timeSpanTrigger"/>, <see cref="countTrigger"/> and <see cref="anomalyCountTrigger"/> are not applicable.</exception>
        public TriggerSettings(
            TimeSpan timeSpanTrigger,
            long countTrigger,
            long initialCountTrigger,
            long anomalyCountTrigger)
        {
            TimeSpanTrigger = timeSpanTrigger;
            CountTrigger = countTrigger;
            InitialCountTrigger = initialCountTrigger;
            AnomalyCountTrigger = anomalyCountTrigger;

            ApplyTimeSpanTrigger = TimeSpanTrigger.TotalSeconds > 0;
            ApplyCountTrigger = CountTrigger > 0;
            ApplyInitialCountTrigger = InitialCountTrigger > 0;
            ApplyAnomalyCountTrigger = AnomalyCountTrigger > 0;

            if (!(ApplyAnomalyCountTrigger || ApplyCountTrigger || ApplyTimeSpanTrigger))
            {
                throw new ArgumentException($"Either {TimeSpanTrigger}, {CountTrigger} or {AnomalyCountTrigger} must be applied.");
            }
        }

        /// <inheritdoc></inheritdoc>
        public void SetInitialTriggerUsed()
        {
            ApplyInitialCountTrigger = false;
        }
    }
}