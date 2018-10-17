namespace SWE.Reflection.Utilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionUtilities
    {
        /// <summary>
        /// Determines <see cref="T"/> to be a nullable type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsNullable<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), nameof(T));
            var property = GetPropertyExpression(parameter, propertyName);
            var type = property.Type;
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsClass)
            {
                return true;
            }

            return typeInfo.IsValueType
                && type.IsConstructedGenericType
                && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static string GetPropertyNameFromPath(string property)
        {
            return property.Split('.').Last();
        }

        private static Expression GetPropertyExpression(ParameterExpression parameter, string propertyName)
        {
            return propertyName.Split('.').Aggregate<string, Expression>(parameter, Expression.Property);
        }
    }
}