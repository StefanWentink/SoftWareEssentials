namespace SWE.BasicType.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            return value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? value.ToString();
        }
    }
}