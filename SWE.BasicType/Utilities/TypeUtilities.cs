namespace SWE.BasicType.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class TypeUtilities
    {
        public static T? ToNullableTypeOrNull<T>(object value)
            where T : struct
        {
            return ToNullableTypeOrDefault<T>(value, null);
        }

        public static T? ToNullableTypeOrDefault<T>(object value)
            where T : struct
        {
            return ToNullableTypeOrDefault<T>(value, default(T));
        }

        public static T? ToNullableTypeOrDefault<T>(object value, T? defaultValue)
            where T : struct
        {
            if (value is T typedValue)
            {
                return typedValue;
            }

            return defaultValue;
        }

        public static T ToTypeOrDefault<T>(object value)
            where T : struct
        {
            return ToTypeOrDefault<T>(value, default);
        }

        public static T ToTypeOrDefault<T>(object value, T defaultValue)
            where T : struct
        {
            if (value is T typedValue)
            {
                return typedValue;
            }

            return defaultValue;
        }
    }
}