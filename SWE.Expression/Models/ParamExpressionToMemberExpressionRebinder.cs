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
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            return base.Visit(node == null || node == _paramExpression
                                  ? _memberExpression
                                  : node);
        }
    }
}