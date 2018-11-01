namespace SWE.Reflection.Extensions
{
    using System;
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
        /// Returning <see cref="expression"/> as <see cref="MemberInfo"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If <see cref="expression"/> is null.</exception>
        /// <exception cref="ArgumentException">If <see cref="expression"/> lambda not of type <see cref="ExpressionType.Convert"/> or <see cref="ExpressionType.MemberAccess"/>.</exception>
        public static MemberExpression GetMemberInfo<T, TValue>(this Expression<Func<T, TValue>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression is LambdaExpression lambda)
            {
                if (lambda.Body.NodeType == ExpressionType.Convert)
                {
                    return ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                }

                if (lambda.Body.NodeType == ExpressionType.MemberAccess)
                {
                    return lambda.Body as MemberExpression;
                }
            }

            throw new ArgumentException($"{nameof(expression)} could not be cast.", nameof(expression));
        }
    }
}