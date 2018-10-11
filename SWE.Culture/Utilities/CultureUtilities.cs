namespace SWE.Culture.Utilities
{
    using System;
    using System.Globalization;

    public static class CultureUtilities
    {
        private const char LanguageCultureSeparatorChar = '-';

        private const string LanguageCultureSeparator = "-";

        private static void CutureCodeLengthValidation(string cultureCode)
        {
            if (cultureCode.Length != 2 && cultureCode.Length != 5)
            {
                throw new ArgumentException($"Length of {nameof(CultureInfo)} must be either 2 or 5.", nameof(cultureCode));
            }
        }

        public static string CleanCutureCode(string cultureCode)
        {
            var result = cultureCode.Trim().Replace(" ", string.Empty);
            CutureCodeLengthValidation(result);
            return result
                .Replace("_", LanguageCultureSeparator)
                .Replace(".", LanguageCultureSeparator);
        }

        public static (string languageCode, string localeCode) SplitCultureCodeParts(string cultureCode)
        {
            var result = CleanCutureCode(cultureCode);

            if (result.Contains(LanguageCultureSeparator))
            {
                var parts = result.Split(LanguageCultureSeparatorChar);
                return (parts[0].ToLowerInvariant(), parts[1].ToUpperInvariant());
            }

            return (result, string.Empty);
        }

        public static string ConcatCultureCodeParts(string languageCode, string localeCode)
        {
            return languageCode.ToLowerInvariant() +
                   (string.IsNullOrWhiteSpace(localeCode)
                       ? string.Empty
                       : LanguageCultureSeparator + localeCode.ToUpperInvariant());
        }
    }
}