namespace SWE.BasicType.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumUtilities
    {
        /// <summary>
        /// Get all <see cref="TEnum"/> values respecting <see cref="func"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetValues<TEnum>(Func<TEnum, bool> function)
            where TEnum : struct, IConvertible
        {
            return GetValues<TEnum>().Where(function);
        }

        /// <summary>
        /// Get all <see cref="TEnum"/> values.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetValues<TEnum>()
            where TEnum : struct, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Gets string representation of enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetDescription(true);
        }

        /// <summary>
        /// Gets string representation of enum value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="getDescriptionAttributeIfAvailabe">Get DescriptionAttribute </param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, bool readAttribute)
        {
            return readAttribute
                ? value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString()
                : value.ToString();
        }

        /// <summary>
        /// Get (first) <see cref="TAttribute"/> for the Enum <see cref="value"/>.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var fieldInfo = value
                .GetType()
                .GetField(value.ToString());

            var attributes = (TAttribute[])fieldInfo.GetCustomAttributes(typeof(TAttribute), false);

            return attributes.Length > 0
                ? attributes[0]
                : null;
        }

        /// <summary>
        /// Parse Description <see cref="value"/> to Enum element <see cref="TEnum"/>.
        /// Returns default value of <see cref="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(string value)
        {
            return ParseEnum<TEnum>(value, default);
        }

        /// <summary>
        /// Parse <see cref="DescriptionAttribute"/> <see cref="value"/> to <see cref="TEnum"/>.
        /// Returns <paramref name="defaultValue"/> when no <see cref="TEnum"/> found.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(string value, TEnum defaultValue)
        {
            foreach (var fieldInfo in typeof(TEnum).GetFields())
            {
                foreach (var attribute in fieldInfo.GetCustomAttributes(false))
                {
                    if (attribute is DescriptionAttribute descriptionAttribute &&
                        descriptionAttribute.Description == value)
                    {
                        return (TEnum)Enum.Parse(typeof(TEnum), fieldInfo.Name);
                    }
                }
            }

            return defaultValue;
        }
    }
}