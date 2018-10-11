namespace SWE.Reflection.Test
{
    using global::Xunit;
    using SWE.Reflection.Extensions;
    using SWE.Reflection.Test.Data;
    using SWE.Xunit.Attributes;
    using System;
    using System.Linq.Expressions;

    public class ReflectionExtensionsTest
    {
        private const int IntPropertyValue = 23;

        private const double DoublePropertyValue = 34.5;

        private const string StringPropertyValue = "Original";

        private static readonly Guid GuidPropertyValue = Guid.NewGuid();

        private static readonly DateTimeOffset DateTimeOffsetPropertyValue = new DateTimeOffset(2018, 1, 2, 3, 4, 5, TimeSpan.FromHours(6));

        private ReflectionStub GetReflectionStub()
        {
            return new ReflectionStub(IntPropertyValue, DoublePropertyValue, StringPropertyValue, GuidPropertyValue, DateTimeOffsetPropertyValue);
        }

        [Fact]
        [Category("ReflectionExtensions")]
        public void MemberSelector_Should_ReturnSelector()
        {
            var selector = ReflectionExtensions.MemberSelector<ReflectionStub>(typeof(ReflectionStub), nameof(ReflectionStub.IntProperty));
            var item = GetReflectionStub();
            var actual = selector(item);
            Assert.Equal(item.IntProperty, actual);
        }

        [Fact]
        [Category("ReflectionExtensions")]
        public void MemberSelector_Should_ReturnSelector_WhenItemParsed()
        {
            var item = GetReflectionStub();
            var selector = ReflectionExtensions.MemberSelector<ReflectionStub>(item, nameof(ReflectionStub.IntProperty));
            var actual = selector(item);
            Assert.Equal(item.IntProperty, actual);
        }

        [Fact]
        [Category("ReflectionExtensions")]
        public void MemberSelector_Should_ThrowException_WhenTypeMismatched()
        {
            var item = GetReflectionStub();
            var selector = ReflectionExtensions.MemberSelector<ReflectionStub>(item, nameof(ReflectionStub.StringProperty));
            var actual = selector(item);
            Assert.NotEqual(item.IntProperty, actual);
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

        [Fact]
        public void IsNullable_Should_ReturnFalse_When_Int()
        {
            Assert.False(ReflectionExtensions.IsNullable<ReflectionStub>(nameof(ReflectionStub.IntProperty)));
        }

        [Fact]
        public void IsNullable_Should_ReturnTrue_When_NullableInt()
        {
            Assert.True(ReflectionExtensions.IsNullable<ReflectionStub>(nameof(ReflectionStub.NullableIntProperty)));
        }

        [Fact]
        public void IsNullable_Should_ReturnFalse_When_Collection()
        {
            Assert.False(ReflectionExtensions.IsNullable<ReflectionStub>(nameof(ReflectionStub.Reflections)));
        }

        [Fact]
        public void IsNullable_Should_ReturnTrue_When_Class()
        {
            Assert.True(ReflectionExtensions.IsNullable<ReflectionStub>(nameof(ReflectionStub.Reflection)));
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