namespace SWE.Reflection.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        /// <summary>
        /// Get expression for selecting a property by <see cref="propertyName"/> on <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Func<T, object> MemberSelector<T>(T value, string propertyName)
        {
            return MemberSelector<T>(value.GetType(), propertyName);
        }

        /// <summary>
        /// Get expression for selecting a property by <see cref="propertyName"/> on <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="valueType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Func<T, object> MemberSelector<T>(Type type, string propertyName)
        {
            var property = TypeExtensions.GetProperty(type, propertyName);

            var parameterExpression = Expression.Parameter(typeof(object), "obj");

            return (Func<T, object>)Expression.Lambda(Expression.
                TypeAs(Expression.Property(Expression.
                    TypeAs(parameterExpression, type), property), typeof(object)), parameterExpression).Compile();
        }

        /// <summary>
        /// Get an expression that can set a property based on <see cref="selectorExpression"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue">Value to be set.</typeparam>
        /// <param name="selectorExpression"></param>
        /// <returns>Returns a function.</returns>
        public static Expression<Action<T, TValue>> SetValueExpression<T, TValue>(this Expression<Func<T, TValue>> selectorExpression)
        {
            var entityParameterExpression = (ParameterExpression)((MemberExpression)selectorExpression.Body).Expression;

            var valueParameterExpression = Expression.Parameter(typeof(TValue));

            return Expression.Lambda<Action<T, TValue>>(
                Expression.Assign(selectorExpression.Body, valueParameterExpression),
                entityParameterExpression,
                valueParameterExpression);
        }

        /// <summary>
        /// Get an expression that can set a property based on <see cref="selectorExpression"/>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="selectorExpression"></param>
        /// <param name="value"></param>
        /// <returns>Returns a function.</returns>
        public static Expression<Action<TModel>> SetValueExpression<TModel, TValue>(
            this Expression<Func<TModel, TValue>> selectorExpression,
            TValue value)
        {
            Expression<Func<TValue>> valueExpression = () => value;

            return SetValueExpression(selectorExpression, valueExpression);
        }

        /// <summary>
        /// Get an expression that can set a property based on <see cref="selectorExpression"/>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue">Value to be set.</typeparam>
        /// <param name="selectorExpression"></param>
        /// <param name="valueExpression"></param>
        /// <returns>Returns a function.</returns>
        public static Expression<Action<TModel>> SetValueExpression<TModel, TValue>(
            this Expression<Func<TModel, TValue>> selectorExpression,
            Expression<Func<TValue>> valueExpression)
        {
            var entityParameterExpression = (ParameterExpression)
                ((MemberExpression)selectorExpression.Body).Expression;

            return Expression.Lambda<Action<TModel>>(
                Expression.Assign(selectorExpression.Body, valueExpression.Body),
                entityParameterExpression);
        }

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

        private static Expression GetPropertyExpression(ParameterExpression parameter, string propertyName)
        {
            return propertyName.Split('.').Aggregate<string, Expression>(parameter, Expression.Property);
        }
    }
}