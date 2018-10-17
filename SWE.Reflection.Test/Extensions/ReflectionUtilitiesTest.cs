namespace SWE.Reflection.Test.Extensions
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Reflection.Extensions;
    using SWE.Reflection.Test.Data;
    using SWE.Reflection.Utilities;
    using SWE.Xunit.Attributes;
    using System;
    using System.Linq.Expressions;

    public class ReflectionUtilitiesTest
    {
        [Fact]
        [Category("ReflectionUtilities")]
        public void IsNullable_Should_ReturnFalse_When_Int()
        {
            Assert.False(ReflectionUtilities.IsNullable<ReflectionStub>(nameof(ReflectionStub.IntProperty)));
        }

        [Fact]
        [Category("ReflectionUtilities")]
        public void IsNullable_Should_ReturnTrue_When_NullableInt()
        {
            Assert.True(ReflectionUtilities.IsNullable<ReflectionStub>(nameof(ReflectionStub.NullableIntProperty)));
        }

        [Fact]
        [Category("ReflectionUtilities")]
        public void IsNullable_Should_ReturnFalse_When_Collection()
        {
            Assert.False(ReflectionUtilities.IsNullable<ReflectionStub>(nameof(ReflectionStub.Reflections)));
        }

        [Fact]
        [Category("ReflectionUtilities")]
        public void IsNullable_Should_ReturnTrue_When_Class()
        {
            Assert.True(ReflectionUtilities.IsNullable<ReflectionStub>(nameof(ReflectionStub.Reflection)));
        }
    }
}