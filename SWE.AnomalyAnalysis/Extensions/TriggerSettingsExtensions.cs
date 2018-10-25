namespace SWE.AnomalyAnalysis.Extensions
{
    using SWE.AnomalyAnalysis.Enums;
    using SWE.AnomalyAnalysis.Interfaces;
    using System;

    internal static class TriggerSettingsExtensions
    {
        internal static Trigger DetermineTrigger(
            this ITriggerSettings triggerSettings,
            DateTimeOffset referenceDate,
            DateTimeOffset nextCalculationDate,
            long count)
        {
            if (triggerSettings.ApplyCountTrigger && count >= triggerSettings.CountTrigger)
            {
                return Trigger.Count;
            }

            if (triggerSettings.ApplyTimeSpanTrigger && referenceDate >= nextCalculationDate)
            {
                return Trigger.Time;
            }

            return Trigger.None;
        }

        internal static Trigger DetermineAnomalyTrigger(
            this ITriggerSettings triggerSettings,
            long anomalyCount)
        {
            if (triggerSettings.ApplyAnomalyCountTrigger && anomalyCount >= triggerSettings.AnomalyCountTrigger)
            {
                return Trigger.Anomaly;
            }

            return Trigger.None;
        }
    }
}