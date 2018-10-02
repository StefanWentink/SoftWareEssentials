namespace SWE.Expression.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using SWE.Expression.Models;

    public static class ExpressionExtensions
    {
        /// <summary>
        /// Applies a type based <see cref="propertySelector"/> and <see cref="propertyPredicate"/> to a single expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="propertySelector"></param>
        /// <param name="propertyPredicate"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineSelectorParamExpression<T, TSelector>(
            this Expression<Func<T, TSelector>> propertySelector,
            Expression<Func<TSelector, bool>> propertyPredicate)
        {
            if (!(propertySelector.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("propertySelector");
            }

            var expression = Expression.Lambda<Func<T, bool>>(propertyPredicate.Body, propertySelector.Parameters);
            var rebinder = new ParamExpressionToMemberExpressionRebinder(propertyPredicate.Parameters[0], memberExpression);
            expression = (Expression<Func<T, bool>>)rebinder.Visit(expression);

            return expression;
        }

        /// <summary>
        /// Combine two expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="additionalExpression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineExpressionAnd<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> additionalExpression)
        {
            return expression.CombineExpression(additionalExpression, false);
        }

        /// <summary>
        /// Combine two expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="additionalExpression"></param>
        /// <returns></returns>
        public static Expression<Func<TResult, bool>> CombineSubExpressionAnd<T, TResult>(this Expression<Func<T, bool>> expression, Expression<Func<TResult, bool>> additionalExpression)
            where TResult : T
        {
            return expression.CombineSubExpression(additionalExpression, false);
        }

        /// <summary>
        /// Combine two expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="additionalExpression"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineExpressionOr<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> additionalExpression)
        {
            return expression.CombineExpression(additionalExpression, true);
        }

        /// <summary>
        /// Combine two expressions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="additionalExpression"></param>
        /// <returns></returns>
        public static Expression<Func<TResult, bool>> CombineSubExpressionOr<T, TResult>(this Expression<Func<T, bool>> expression, Expression<Func<TResult, bool>> additionalExpression)
            where TResult : T
        {
            return expression.CombineSubExpression(additionalExpression, true);
        }

        /// <summary>
        /// Change the <see cref="T"/> parameter of an expression to <see cref="TResult"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<TResult, bool>> CastSubExpression<T, TResult>(this Expression<Func<T, bool>> expression)
            where TResult : T
        {
            return expression.CombineSubExpressionOr<T, TResult>(null);
        }

        private static Expression<Func<T, bool>> CombineExpression<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, bool>> additionalExpression, bool or)
        {
            if (expression == null)
            {
                return additionalExpression;
            }

            if (additionalExpression == null)
            {
                return expression;
            }

            return or ? expression.Or(additionalExpression) : expression.And(additionalExpression);
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        private static Expression<Func<TResult, bool>> ComposeSub<T, TResult>(
            this Expression<Func<T, bool>> expression,
            Expression<Func<TResult, bool>> additionalExpression,
            bool and)
            where TResult : T
        {
            var parameter = Expression.Parameter(typeof(TResult));

            var leftVisitor = new ReplaceExpressionVisitor(expression.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression.Body);

            var secondaryExpression = additionalExpression;
            var expressionOperatorAnd = and;

            if (secondaryExpression == null)
            {
                secondaryExpression = x => true;
                expressionOperatorAnd = true;
            }

            var rightVisitor = new ReplaceExpressionVisitor(secondaryExpression.Parameters[0], parameter);
            var right = rightVisitor.Visit(secondaryExpression.Body);

            if (left == null)
            {
                return secondaryExpression;
            }

            return Expression.Lambda<Func<TResult, bool>>(
                expressionOperatorAnd
                ? Expression.AndAlso(left, right)
                : Expression.OrElse(left, right),
                parameter);
        }

        private static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        private static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        private static Expression<Func<TResult, bool>> CombineSubExpression<T, TResult>(this Expression<Func<T, bool>> expression, Expression<Func<TResult, bool>> additionalExpression, bool or)
            where TResult : T
        {
            if (expression == null)
            {
                return additionalExpression;
            }

            return or ? expression.OrSub(additionalExpression) : expression.AndSub(additionalExpression);
        }

        private static Expression<Func<TResult, bool>> AndSub<T, TResult>(this Expression<Func<T, bool>> first, Expression<Func<TResult, bool>> second)
            where
            TResult : T
        {
            return ComposeSub(first, second, true);
        }

        private static Expression<Func<TResult, bool>> OrSub<T, TResult>(this Expression<Func<T, bool>> first, Expression<Func<TResult, bool>> second)
            where TResult : T
        {
            return first.ComposeSub(second, false);
        }
    }
}
