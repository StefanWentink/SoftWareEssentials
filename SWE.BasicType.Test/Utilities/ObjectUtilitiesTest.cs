namespace SWE.BasicType.Test.Utilities
{
    using global::Xunit;
    using SWE.BasicType.Test.Data.Models;
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;
    using System;

    public class ObjectUtilitiesTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0.00000001)]
        [InlineData("")]
        [InlineData(" ")]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse(object value)
        {
            Assert.False(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_StringEmpty()
        {
            Assert.False(ObjectUtilities.IsDefault(string.Empty));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_DateTime()
        {
            var value = new DateTime(2018, 1, 1);
            Assert.False(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_NullableDateTime()
        {
            DateTime? value = new DateTime();
            Assert.False(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_DateTimeOffset()
        {
            var value = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.FromHours(1));
            Assert.False(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_NullableDateTimeOffset()
        {
            DateTimeOffset? value = new DateTimeOffset();
            Assert.False(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnFalse_When_Class()
        {
            Assert.False(ObjectUtilities.IsDefault(new ClassStub()));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenInt()
        {
            const int value = 0;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenNullableInt()
        {
            int? value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenDouble()
        {
            const double value = 0;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenNullableDouble()
        {
            double? value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenDecimal()
        {
            const decimal value = 0;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_WhenNullableDecimal()
        {
            decimal? value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_StringNull()
        {
            const string value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_DateTime()
        {
            var value = new DateTime();
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_NullableDateTime()
        {
            DateTime? value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_DateTimeOffset()
        {
            var value = new DateTimeOffset();
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_NullableDateTimeOffset()
        {
            DateTimeOffset? value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_Class()
        {
            ClassStub value = null;
            Assert.True(ObjectUtilities.IsDefault(value));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_StructNew()
        {
            Assert.True(ObjectUtilities.IsDefault(new StructStub()));
        }

        [Fact]
        [Category("ObjectUtilities")]
        public void IsDefault_Should_ReturnTrue_When_StructNotSet()
        {
            StructStub value;
            Assert.True(ObjectUtilities.IsDefault(value));
        }
    }
}