namespace SWE.BasicType.Utilities
{
    public static class ObjectUtilities
    {
        /// <summary>
        /// Determines if <see cref="value"/> is default for <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefault<T>(T value)
        {
            return Equals(value, default(T));
        }
    }
}