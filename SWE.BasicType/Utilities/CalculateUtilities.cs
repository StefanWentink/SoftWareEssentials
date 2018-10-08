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
        /// <param name="_result">Returns <see cref="default"/> if operation fails.</param>
        /// <returns>True if subtract succeeded.</returns>
        public static bool TrySubtract<TResult, TValue>(TResult baseValue, TValue value, out TResult _result)
        {
            _result = default;

            try
            {
                _result = (dynamic)baseValue - (dynamic)value;
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
        /// <param name="_result">Returns <see cref="default"/> if operation fails.</param>
        /// <returns>True if add succeeded.</returns>
        public static bool TryAdd<TResult, TValue>(TResult baseValue, TValue value, out TResult _result)
        {
            _result = default;

            try
            {
                _result = (dynamic)baseValue + (dynamic)value;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to invert <see cref="value"/>>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="baseValue"></param>
        /// <param name="value"></param>
        /// <param name="_result">Returns <see cref="default"/> if operation fails.</param>
        /// <returns>True if invert succeeded.</returns>
        public static bool TryInvert<TValue>(TValue value, out TValue _result)
        {
            _result = default;

            try
            {
                _result = (dynamic)value * -1;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}