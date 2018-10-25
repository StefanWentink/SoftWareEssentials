namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;

    /// <summary>
    /// Preserve after calculation settings.
    /// </summary>
    public interface IHistorySettings
    {
        /// <summary>
        /// Preserves <see cref="PreserveTimeSpan"/> after calculation.
        /// </summary>
        TimeSpan PreserveTimeSpan { get; }

        /// <summary>
        /// Preserves <see cref="PreserveCount"/> after calculation.
        /// </summary>
        long PreserveCount { get; }
    }
}