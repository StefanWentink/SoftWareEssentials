namespace SWE.AnomalyAnalysis.Interfaces
{
    using System;

    /// <summary>
    /// Calculation trigger settings.
    /// </summary>
    public interface ITriggerSettings
    {
        bool ApplyInitialCountTrigger { get; }

        bool ApplyCountTrigger { get; }

        bool ApplyAnomalyCountTrigger { get; }

        bool ApplyTimeSpanTrigger { get; }

        /// <summary>
        /// (Re)calculate initially after <see cref="AnomalyCountTrigger"/> anomalies.
        /// </summary>
        long AnomalyCountTrigger { get; }

        /// <summary>
        /// (Re)calculate initially after <see cref="CountTrigger"/> times.
        /// </summary>
        long CountTrigger { get; }

        /// <summary>
        /// Calculate initially after <see cref="InitialCountTrigger"/> times.
        /// </summary>
        long InitialCountTrigger { get; }

        /// <summary>
        /// (Re)calculate after <see cref="TimeSpanTrigger"/> <see cref="TimeSpan"/>.
        /// </summary>
        TimeSpan TimeSpanTrigger { get; }

        /// <summary>
        /// Use of initial trigger registered
        /// </summary>
        void SetInitialTriggerUsed();
    }
}