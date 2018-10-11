namespace SWE.BasicType.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumUtilities
    {
        /// <summary>
        /// Get all <see cref="TEnum"/> values respecting <see cref="func"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetValues<TEnum>(Func<TEnum, bool> func)
            where TEnum : struct, IConvertible
        {
            return GetValues<TEnum>().Where(func);
        }

        /// <summary>
        /// Get all <see cref="TEnum"/> values.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
    }
}