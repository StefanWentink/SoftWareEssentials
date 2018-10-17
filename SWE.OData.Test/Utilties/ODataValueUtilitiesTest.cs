namespace SWE.OData.Test.Utilties
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.OData.Utilities;
    using FluentAssertions;
    using System;

    public class ODataValueUtilitiesTest
    {
        [Fact]
        [Category("ODataValueUtilities")]
        public void ODataBuilder_Should_ReturnValueAsString_When_Int()
        {
            var actual = ODataValueUtilities.ParameterToString(3);
            actual.Should().Be("3");
        }

        [Fact]
        [Category("ODataValueUtilities")]
        public void ODataBuilder_Should_ReturnValueAsString_When_Double()
        {
            var actual = ODataValueUtilities.ParameterToString(3.4);
            actual.Should().Be("3.4");
        }

        [Fact]
        [Category("ODataValueUtilities")]
        public void ODataBuilder_Should_ReturnValueAsString_When_String()
        {
            var actual = ODataValueUtilities.ParameterToString("3");
            actual.Should().Be("'3'");
        }

        [Fact]
        [Category("ODataValueUtilities")]
        public void ODataBuilder_Should_ReturnValueAsString_When_Guid()
        {
            var key = Guid.NewGuid();
            var actual = ODataValueUtilities.ParameterToString(key);
            actual.Should().Be($"({key})");
        }

        [Fact]
        [Category("ODataValueUtilities")]
        public void ODataBuilder_Should_ReturnValueAsString_When_GuidKey()
        {
            var key = Guid.NewGuid();
            var actual = ODataValueUtilities.ParameterToString(key, true);
            actual.Should().Be($"{key}");
        }
    }
}