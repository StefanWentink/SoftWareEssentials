namespace SWE.BasicType.Date.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using MoreLinq;

    using NodaTime.TimeZones;
    using NodaTime.TimeZones.Cldr;

    public static class TimeZoneInfoUtilities
    {
        public static ReadOnlyCollection<TimeZoneInfo> Zones { get; } = TimeZoneInfo.GetSystemTimeZones();

        public static TimeZoneInfo DutchTimeZoneInfo { get; } = GetDutchTimeZoneInfo();

        /// <summary>
        /// Gets <see cref="TimeZoneInfo"/> from SystemTimeZones
        /// </summary>
        /// <param name="timeZoneId">Can be fetched from a <see cref="MapZone"/>.WindowsId. </param>
        /// <returns>Returns <see cref="TimeZoneInfo.Utc"/> if <see cref="timeZoneId"/> not found.</returns>
        public static TimeZoneInfo GetTimeZoneInfoByTimeZoneId(string timeZoneId)
        {
            var result = Zones.SingleOrDefault(x => x.Id == timeZoneId);

            return result ?? TimeZoneInfo.Utc;
        }

        /// <summary>
        /// Gets <see cref="TimeZoneInfo"/> from SystemTimeZones
        /// </summary>
        /// <param name="countryCode">2 letter countryCode</param>
        /// <returns></returns>
        /// <returns></returns>
        /// <exception cref="ArgumentException">No results in collection for countryCode</exception>
        /// <exception cref="ArgumentException">Multiple results in collection for countryCode</exception>
        public static TimeZoneInfo GetTimeZoneInfoByCountryCode(string countryCode)
        {
            return GetTimeZoneInfosByCountryCode(countryCode)
                .DistinctBy(x => x.BaseUtcOffset)
                .ToList()
                .ValidateSingleResultForCountryCode(countryCode);
        }

        /// <summary>
        /// Gets a list of <see cref="TimeZoneInfo"/> from SystemTimeZones
        /// </summary>
        /// <param name="countryCode">2 letter countryCode</param>
        /// <returns></returns>
        public static IEnumerable<TimeZoneInfo> GetTimeZoneInfosByCountryCode(string countryCode)
        {
            var mapZone = GetMapZonesByCountryCode(countryCode);
            return mapZone
                .Select(x => x.WindowsId)
                .Distinct()
                .Select(GetTimeZoneInfoByTimeZoneId)
                .Distinct();
        }

        /// <summary>
        /// Gets a list of <see cref="MapZone"/> for countryCode
        /// </summary>
        /// <param name="countryCode">2 letter countryCode</param>
        /// <returns></returns>
        public static List<MapZone> GetMapZonesByCountryCode(string countryCode)
        {
            return GetTimeZonesByCountryCode(countryCode)
                .Select(z => z.ZoneId)
                .Distinct()
                .SelectMany(x =>
                    TzdbDateTimeZoneSource.Default.CanonicalIdMap
                        .Where(c => c.Value.Equals(x))
                        .Select(c => c.Key))
                .SelectMany(x => TzdbDateTimeZoneSource.Default.WindowsMapping.MapZones
                    .Where(mz => mz.TzdbIds.Contains(x)))
                .ToList();
        }

        /// <summary>
        /// Gets Available <see cref="TzdbZoneLocation"/> list for countryCode
        /// </summary>
        /// <param name="countryCode">2 letter countryCode</param>
        /// <returns></returns>
        public static List<TzdbZoneLocation> GetTimeZonesByCountryCode(string countryCode)
        {
            return (TzdbDateTimeZoneSource.Default.ZoneLocations ?? throw new InvalidOperationException("ZoneLocations not initialized"))
                .Where(x => x.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private static TimeZoneInfo GetDutchTimeZoneInfo()
        {
            return GetTimeZoneInfoByCountryCode("NL");
        }

        private static T ValidateSingleResultForCountryCode<T>(this IReadOnlyCollection<T> values, string countryCode)
        {
            if (values.Count > 0)
            {
                if (values.Count == 1)
                {
                    return values.Single();
                }

                throw new ArgumentException($"Multiple results in collection for countryCode '{countryCode}'", nameof(countryCode));
            }

            throw new ArgumentException($"No results in collection for countryCode '{countryCode}'", nameof(countryCode));
        }
    }
}