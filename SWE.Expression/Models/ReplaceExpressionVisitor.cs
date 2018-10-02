namespace SWE.Expression.Models
{
    using System.Linq.Expressions;

    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private Expression OriginalExpression { get; }

        private Expression ReplacementExpression { get; }

        internal ReplaceExpressionVisitor(Expression originalExpression, Expression replacementExpression)
        {
            OriginalExpression = originalExpression;
            ReplacementExpression = replacementExpression;
        }

        public override Expression Visit(Expression node)
        {
            return node == OriginalExpression
                ? ReplacementExpression
                : base.Visit(node);
        }
    }
}