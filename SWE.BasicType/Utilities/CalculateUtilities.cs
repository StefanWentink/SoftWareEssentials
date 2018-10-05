namespace SWE.BasicType.Utilities
{
    public static class CalculateUtilities
    {
        /// <summary>
        /// Tries to subtract <see cref="value"/> from <see cref="baseValue"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="baseValue"></param>
        /// <param name="value"></param>
        /// <param name="result">Returns <see cref="default"/> if operation fails.</param>
        /// <returns></returns>
        public static bool TrySubtract<TResult, TValue>(TResult baseValue, TValue value, out TResult result)
        {
            result = default;

            try
            {
                result = (dynamic)baseValue - (dynamic)value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to add <see cref="value"/> to <see cref="baseValue"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="baseValue"></param>
        /// <param name="value"></param>
        /// <param name="result">Returns <see cref="default"/> if operation fails.</param>
        public static bool TryAdd<TResult, TValue>(TResult baseValue, TValue value, out TResult result)
        {
            result = default;

            try
            {
                result = (dynamic)baseValue + (dynamic)value;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
