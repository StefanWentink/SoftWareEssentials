using SWE.AnomalyAnalysis.Interfaces;
using SWE.BasicType.Utilities;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SWE.AnomalyAnalysis.Extensions
{
    internal static class HistorySettingsExtension
    {
        /// <summary>
        /// Reduce number of entries in <see cref="settings"/> based on <see cref="IHistorySettings"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="settings"></param>
        /// <param name="values"></param>
        /// <param name="anomalies"></param>
        /// <param name="referenceDate"></param>
        /// <returns></returns>
        internal static long ReduceValues<TValue>(
            this IHistorySettings settings,
            ConcurrentDictionary<DateTimeOffset, TValue> values,
            ConcurrentBag<TValue> anomalies,
            DateTimeOffset referenceDate)
        {
            long result = 0;
            var oldestValue = values.Keys.OrderBy(x => x).FirstOrDefault();

            while (!anomalies.IsEmpty)
            {
                if (!anomalies.TryTake(out var _))
                {
                    return result++;
                }
            }

            if (settings.PreserveTimeSpan > TimeSpan.Zero)
            {
                var preserveDate = referenceDate - settings.PreserveTimeSpan;

                while (!CompareUtilities.IsDefault(oldestValue) && oldestValue < preserveDate)
                {
                    values.TryRemove(oldestValue, out _);
                    oldestValue = values.Keys.OrderBy(x => x).FirstOrDefault();
                    result++;
                }

                return result;
            }

            while (!CompareUtilities.IsDefault(oldestValue) && values.Keys.Count > settings.PreserveCount)
            {
                if (!values.TryRemove(oldestValue, out _))
                {
                    return result;
                }

                oldestValue = values.Keys.OrderBy(x => x).FirstOrDefault();
                result++;
            }

            return result;
        }
    }
}