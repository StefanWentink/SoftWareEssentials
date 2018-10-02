namespace SWE.Expression.Test.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using global::Xunit;

    using SWE.Expression.Extensions;
    using SWE.Expression.Test.Data.Factories;
    using SWE.Expression.Test.Data.Models;
    using SWE.Xunit.Attributes;

    public class ExpressionExtensionsTest
    {
        private readonly Expression<Func<PersonStub, bool>> _personExpression1 = x => x.LastName == "Jansen";

        private readonly Expression<Func<PersonStub, bool>> _personExpression2 = x => x.FirstName.StartsWith("J");

        private readonly Expression<Func<ContactPersonStub, bool>> _contactPersonExpression = x => x.Id > 1;

        private readonly Expression<Func<PersonStub, string>> _personSelector = x => x.FirstName;

        private readonly Expression<Func<string, bool>> _stringExpression = x => x.Length == 3;

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineExpressionAnd_Should_ReturnValidExpression_WhenTypesAreEqual()
        {
            var expression = _personExpression1.CombineExpressionAnd(_personExpression2);

            var actual = PersonFactory.List.Where(expression.Compile());
            Assert.Single(actual);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineExpressionANd_Should_ReturnValidExpression_WhenAdditionalExpressionNull()
        {
            var expression = _personExpression1.CombineExpressionAnd(null);

            var actual = PersonFactory.List.Where(expression.Compile());
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineExpressionOr_Should_ReturnValidExpression_WhenTypesAreEqual()
        {
            var expression = _personExpression1.CombineExpressionOr(_personExpression2);

            var actual = PersonFactory.List.Where(expression.Compile());
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineExpressionOr_Should_ReturnValidExpression_WhenAdditionalExpressionNull()
        {
            var expression = _personExpression1.CombineExpressionOr(null);

            var actual = PersonFactory.List.Where(expression.Compile());
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineSubExpressionAnd_Should_ReturnValidExpression_WhenTypesAreEqual()
        {
            var expression = _personExpression1.CombineSubExpressionAnd(_contactPersonExpression);

            var actual = ContactPersonFactory.List.Where(expression.Compile());
            Assert.Single(actual);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineSubExpressionOr_Should_ReturnValidExpression_WhenTypesAreEqual()
        {
            var expression = _personExpression1.CombineSubExpressionOr(_contactPersonExpression);

            var actual = ContactPersonFactory.List.Where(expression.Compile());
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void CombineSelectorParamExpression_Should_ReturnValidExpression()
        {
            var expression = _personSelector.CombineSelectorParamExpression(_stringExpression);

            var actual = ContactPersonFactory.List.Where(expression.Compile());
            Assert.Single(actual);
        }
    }
}