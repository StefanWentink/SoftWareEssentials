namespace SWE.Expression.Models
{
    using System.Linq.Expressions;

    internal class ParamExpressionToMemberExpressionRebinder : ExpressionVisitor
    {
        private readonly ParameterExpression _paramExpression;

        private readonly Expression _memberExpression;

        internal ParamExpressionToMemberExpressionRebinder(ParameterExpression paramExpression, Expression memberExpression)
        {
            _paramExpression = paramExpression;
            _memberExpression = memberExpression;
        }

        /// <summary>
        /// Rebind param expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override Expression Visit(Expression expression)
        {
            return base.Visit(expression == null || expression == _paramExpression
                                  ? _memberExpression
                                  : expression);
        }
    }
}