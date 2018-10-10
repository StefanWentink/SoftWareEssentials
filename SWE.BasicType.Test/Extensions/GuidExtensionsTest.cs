namespace SWE.BasicType.Test.Extensions
{
    using global::Xunit;
    using SWE.BasicType.Extensions;
    using SWE.Xunit.Attributes;
    using System;

    public class GuidExtensionsTest
    {
        [Fact]
        [Category("GuidExtensions")]
        public void GuidIsNullOrEmpty_Should_ReturnFalse_With_Guid()
        {
            Assert.False(Guid.NewGuid().GuidIsNullOrEmpty());
        }

        [Fact]
        [Category("GuidExtensions")]
        public void GuidIsNullOrEmpty_Should_ReturnTrue_With_Guid()
        {
            Assert.True(Guid.Empty.GuidIsNullOrEmpty());
        }

        [Fact]
        [Category("GuidExtensions")]
        public void GuidIsNullOrEmpty_Should_ReturnFalse_With_NullableGuid()
        {
            Guid? value = Guid.NewGuid();
            Assert.False(value.GuidIsNullOrEmpty());
        }

        [Fact]
        [Category("GuidExtensions")]
        public void GuidIsNullOrEmpty_Should_ReturnTrue_With_NullableGuid_When_Empty()
        {
            Guid? value = Guid.Empty;
            Assert.True(value.GuidIsNullOrEmpty());
        }

        [Fact]
        [Category("GuidExtensions")]
        public void GuidIsNullOrEmpty_Should_ReturnTrue_With_NullableGuid_When_Null()
        {
            Guid? value = null;
            Assert.True(value.GuidIsNullOrEmpty());
        }
    }
}