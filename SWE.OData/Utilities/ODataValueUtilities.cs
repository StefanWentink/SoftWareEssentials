namespace SWE.OData.Utilities
{
    using System;
    using System.Globalization;

    public static class ODataValueUtilities
    {
        public static string ParameterToString<TValue>(TValue value)
        {
            return ParameterToString(value, false);
        }

        public static string ParameterToString<TValue>(TValue value, bool isKey)
        {
            if (!isKey && value is Guid guid)
            {
                return $"({value.ToString().ToLowerInvariant()})";
            }

            if (!isKey && value is Guid?)
            {
                return $"({value?.ToString().ToLowerInvariant()})";
            }

            if (!isKey && value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
            }

            if (!isKey && value is DateTime?)
            {
                return $"({value?.ToString().ToLowerInvariant()})";
            }

            if (!isKey && value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString("yyyy-MM-ddThh:mm:ssZ");
            }

            if (!isKey && value is DateTimeOffset?)
            {
                return value?.ToString().ToLowerInvariant();
            }

            if (!isKey && value is double doubleValue)
            {
                return doubleValue.ToString("G", CultureInfo.InvariantCulture).ToLowerInvariant();
            }

            if (!isKey && value is double?)
            {
                return value?.ToString().ToLowerInvariant();
            }

            if (value is string stringValue)
            {
                return $"'{stringValue}'";
            }

            return value.ToString().ToLowerInvariant();
        }
    }
}