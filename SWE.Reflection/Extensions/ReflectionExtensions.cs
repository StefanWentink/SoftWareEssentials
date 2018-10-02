namespace SWE.Reflection.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class ReflectionExtensions
    {
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
    }
}