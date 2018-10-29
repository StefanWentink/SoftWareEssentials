namespace SWE.AnomalyAnalysis.Models
{
    using SWE.AnomalyAnalysis.Interfaces;
    using System;

    /// <inheritdoc></inheritdoc>
    public class HistorySettings : IHistorySettings
    {
        /// <inheritdoc></inheritdoc>
        public long PreserveCount { get; } = 0;

        /// <inheritdoc></inheritdoc>
        public TimeSpan PreserveTimeSpan { get; } = TimeSpan.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistorySettings"/> class.
        /// </summary>
        /// <param name="preserveCount"></param>
        /// <param name="initialPreserveCount"></param>
        /// <exception cref="ArgumentException">If <see cref="preserveCount"/> is smaller or equal to 0.</exception>
        public HistorySettings(
           long preserveCount)
            : this(TimeSpan.Zero, preserveCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistorySettings"/> class.
        /// </summary>
        /// <param name="preserveTimeSpan"></param>
        /// <param name="anomalyCount"></param>
        /// <exception cref="ArgumentException">If <see cref="preserveTimeSpan"/> is not applicable.</exception>
        public HistorySettings(
            TimeSpan preserveTimeSpan)
            : this(preserveTimeSpan, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistorySettings"/> class.
        /// </summary>
        /// <param name="preserveTimeSpan"></param>
        /// <param name="preserveCount"></param>
        /// <exception cref="ArgumentException">If <see cref="preserveCount"/> is smaller or equal to 0.</exception>
        private HistorySettings(
            TimeSpan preserveTimeSpan,
            long preserveCount)
        {
            if (preserveCount < 0)
            {
                throw new ArgumentException($"{nameof(preserveCount)} should be greater or equal to 0.");
            }

            PreserveTimeSpan = preserveTimeSpan;
            PreserveCount = preserveCount;
        }
    }
}