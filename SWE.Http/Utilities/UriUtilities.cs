namespace SWE.Http.Utilities
{
    public static class UriUtilities
    {
        public static string ConcatUriParts(string value, string concat)
        {
            if (value.EndsWith("/"))
            {
                if (concat.StartsWith("/"))
                {
                    return value + concat.Substring(1);
                }
            }
            else if (!string.IsNullOrWhiteSpace(concat) && !concat.StartsWith("/"))
            {
                return $"{value}/{concat}";
            }

            return value + concat;
        }
    }
}