namespace SWE.Reflection.Test
{
    using System;
    using System.Linq.Expressions;

    using SWE.Reflection.Extensions;
    using SWE.Reflection.Test.Data;

    using Xunit;

    public class ReflectionExtensionsTest
    {
        private static readonly int IntPropertyValue = 23;

        private static readonly double DoublePropertyValue = 34.5;

        private static readonly string StringPropertyValue = "Original";

        private static readonly Guid GuidPropertyValue = Guid.NewGuid();

        private static readonly DateTimeOffset DateTimeOffsetPropertyValue = new DateTimeOffset(2018, 1, 2, 3, 4, 5, TimeSpan.FromHours(6));

        private ReflectionStub GetReflectionStub()
        {
            return new ReflectionStub(IntPropertyValue, DoublePropertyValue, StringPropertyValue, GuidPropertyValue, DateTimeOffsetPropertyValue);
        }

        [Fact]
        public void SetValueExpression_Should_UseExpressionToSetValue_When_Int()
        {
            AssertValue(GetReflectionStub(), x => x.IntProperty, 32);
        }

        [Fact]
        public void SetValueExpression_Should_UseExpressionToSetValue_When_Double()
        {
            AssertValue(GetReflectionStub(), x => x.DoubleProperty, 35.4);
        }

        [Fact]
        public void SetValueExpression_Should_UseExpressionToSetValue_When_String()
        {
            AssertValue(GetReflectionStub(), x => x.StringProperty, "New");
        }

        [Fact]
        public void SetValueExpression_Should_UseExpressionToSetValue_When_Guid()
        {
            AssertValue(GetReflectionStub(), x => x.GuidProperty, Guid.NewGuid());
        }

        [Fact]
        public void SetValueExpression_Should_UseExpressionToSetValue_When_DateTimeOffset()
        {
            AssertValue(GetReflectionStub(), x => x.DateTimeOffsetProperty, new DateTimeOffset(2018, 1, 2, 3, 4, 5, TimeSpan.FromHours(5)));
        }

        private static void AssertValue<TValue>(ReflectionStub reflectionStub, Expression<Func<ReflectionStub, TValue>> selectorExpression, TValue expected)
        where TValue : IEquatable<TValue>
        {
            var selector = selectorExpression.Compile();
            var original = selector(reflectionStub);
            Assert.NotEqual(original, expected);

            var setterExpression = selectorExpression.SetValueExpression();
            setterExpression.Compile()(reflectionStub, expected);
            var actual = selector(reflectionStub);
            Assert.NotEqual(original, actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SetValueExpressionWithValue_Should_UseExpressionToSetValue_When_Int()
        {
            AssertSetValueExpression(GetReflectionStub(), x => x.IntProperty, 32);
        }

        [Fact]
        public void SetValueExpressionWithValue_Should_UseExpressionToSetValue_When_Double()
        {
            AssertValue(GetReflectionStub(), x => x.DoubleProperty, 35.4);
        }

        [Fact]
        public void SetValueExpressionWithValue_Should_UseExpressionToSetValue_When_String()
        {
            AssertSetValueExpression(GetReflectionStub(), x => x.StringProperty, "New");
        }

        [Fact]
        public void SetValueExpressionWithValue_Should_UseExpressionToSetValue_When_Guid()
        {
            AssertSetValueExpression(GetReflectionStub(), x => x.GuidProperty, Guid.NewGuid());
        }

        [Fact]
        public void SetValueExpressionWithValue_Should_UseExpressionToSetValue_When_DateTimeOffset()
        {
            AssertSetValueExpression(GetReflectionStub(), x => x.DateTimeOffsetProperty, new DateTimeOffset(2018, 1, 2, 3, 4, 5, TimeSpan.FromHours(5)));
        }

        private static void AssertSetValueExpression<TValue>(ReflectionStub reflectionStub, Expression<Func<ReflectionStub, TValue>> selectorExpression, TValue expected)
            where TValue : IEquatable<TValue>
        {
            var selector = selectorExpression.Compile();
            var original = selector(reflectionStub);
            Assert.NotEqual(original, expected);

            var setterExpression = selectorExpression.SetValueExpression(expected);
            setterExpression.Compile()(reflectionStub);
            var actual = selector(reflectionStub);
            Assert.NotEqual(original, actual);
            Assert.Equal(expected, actual);
        }
    }
}
