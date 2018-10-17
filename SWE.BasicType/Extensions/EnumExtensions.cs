namespace SWE.BasicType.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;

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