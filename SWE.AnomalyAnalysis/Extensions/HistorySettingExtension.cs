using SWE.AnomalyAnalysis.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SWE.AnomalyAnalysis.Extensions
{
    internal static class HistorySettingExtension
    {
        /// <summary>
        /// Reduce number of entries in <see cref="settings"/> based on <see cref="IHistorySettings"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="settings"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static long ReduceValues<TValue>(
            this IHistorySettings settings,
            ConcurrentDictionary<DateTimeOffset, TValue> values,
            DateTimeOffset referenceDate)
        {
            long result = 0;
            var oldestValue = values.Keys.OrderBy(x => x).First();

            if (settings.PreserveTimeSpan > TimeSpan.Zero)
            {
                var preserveDate = referenceDate - settings.PreserveTimeSpan;

                while (oldestValue < preserveDate)
                {
                    values.TryRemove(oldestValue, out _);
                    oldestValue = values.Keys.OrderBy(x => x).First();
                    result++;
                }

                return result;
            }

            while (values.Keys.Count > settings.PreserveCount)
            {
                values.TryRemove(oldestValue, out _);
                oldestValue = values.Keys.OrderBy(x => x).First();
                return result;
            }

            return result;
        }
    }
}