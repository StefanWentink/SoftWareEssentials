namespace SWE.BasicType.Test.Utilities
{
    using global::Xunit;
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;

    public class TypeUtilitiesTest
    {
        [Fact]
        [Category("TypeUtilities")]
        public void ToTypeOrDefault_Should_ReturnValue_WhenOfType_WithInt()
        {
            const int value = 1;
            object objectValue = value;
            Assert.Equal(value, TypeUtilities.ToTypeOrDefault<int>(objectValue));
        }

        [Fact]
        [Category("TypeUtilities")]
        public void ToTypeOrDefault_Should_ReturnDefault_WhenNotOfType_WithInt()
        {
            object objectValue = 1.0;
            Assert.Equal(2, TypeUtilities.ToTypeOrDefault(objectValue, 2));
        }

        [Fact]
        [Category("TypeUtilities")]
        public void ToTypeOrDefault_Should_ReturnDefault_WhenNotOfType_WithNull()
        {
            object objectValue = null;
            Assert.Equal(default, TypeUtilities.ToTypeOrDefault<int>(objectValue));
        }

        [Fact]
        [Category("TypeUtilities")]
        public void ToNullableTypeOrDefault_Should_ReturnValue_WhenOfType_WithInt()
        {
            const int value = 1;
            object objectValue = value;
            Assert.Equal(value, TypeUtilities.ToNullableTypeOrDefault<int>(objectValue));
        }

        [Fact]
        [Category("TypeUtilities")]
        public void ToNullableTypeOrDefault_Should_ReturnDefault_WhenNotOfType_WithInt()
        {
            object objectValue = 1.0;
            Assert.Null(TypeUtilities.ToNullableTypeOrNull<int>(objectValue));
        }

        [Fact]
        [Category("TypeUtilities")]
        public void ToNullableTypeOrDefault_Should_ReturnDefault_WhenNotOfType_WithNull()
        {
            object objectValue = null;
            Assert.Equal(default(int), TypeUtilities.ToNullableTypeOrDefault<int>(objectValue));
        }
    }
}