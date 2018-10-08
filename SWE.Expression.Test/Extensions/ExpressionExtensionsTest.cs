namespace SWE.Expression.Test.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

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
        public void ToSetAction_ShouldSetValue_With_ValueExpressionClosure()
        {
            var person = new PersonStub("Stefan", "Wentink");
            var name = "John";
            var action = ExpressionExtensions.ToSetAction<PersonStub, string>(x => x.FirstName, () => name);
            Assert.Equal("Stefan", person.FirstName);

            action(person);
            Assert.Equal("John", person.FirstName);

            name = "Dick";
            action(person);
            Assert.Equal("Dick", person.FirstName);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void ToSetAction_Should_SetValue_With_Value()
        {
            var person = new PersonStub("Stefan", "Wentink");
            var name = "John";
            var action = ExpressionExtensions.ToSetAction<PersonStub, string>(x => x.FirstName, name);
            Assert.Equal("Stefan", person.FirstName);

            action(person);
            Assert.Equal("John", person.FirstName);

            name = "Dick";
            action(person);
            Assert.Equal("John", person.FirstName);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void ToSetAction_Should_SetValue()
        {
            var person = new PersonStub("Stefan", "Wentink");
            var action = ExpressionExtensions.ToSetAction<PersonStub, string>(x => x.FirstName);
            Assert.Equal("Stefan", person.FirstName);

            action(person, "John");
            Assert.Equal("John", person.FirstName);

            action(person, "Dick");
            Assert.Equal("Dick", person.FirstName);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void ToAddAction_ShouldSetValue_With_ValueExpressionClosure()
        {
            var person = new PersonStub("Stefan", "Wentink", 12);
            var age = 3;
            var action = ExpressionExtensions.ToAddAction<PersonStub, int>(x => x.Age, () => age);
            Assert.Equal(12, person.Age);

            action(person);
            Assert.Equal(15, person.Age);

            age = -5;
            action(person);
            Assert.Equal(10, person.Age);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void ToAddAction_Should_SetValue_With_Value()
        {
            var person = new PersonStub("Stefan", "Wentink", 12);
            var age = 3;
            var action = ExpressionExtensions.ToAddAction<PersonStub, int>(x => x.Age, age);
            Assert.Equal(12, person.Age);

            action(person);
            Assert.Equal(15, person.Age);

            age = -5;
            action(person);
            Assert.Equal(18, person.Age);
        }

        [Fact]
        [Category("ExpressionExtensions")]
        public void ToAddAction_Should_SetValue()
        {
            var person = new PersonStub("Stefan", "Wentink", 12);
            var action = ExpressionExtensions.ToAddAction<PersonStub, int>(x => x.Age);
            Assert.Equal(12, person.Age);

            action(person, 3);
            Assert.Equal(15, person.Age);

            action(person, -5);
            Assert.Equal(10, person.Age);
        }

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