namespace SWE.Reflection.Test.Extensions
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Reflection.Extensions;
    using SWE.Reflection.Test.Data;
    using SWE.Xunit.Attributes;
    using System.Reflection;

    public class AssemblyExtensionsTest
    {
        [Fact]
        [Category("AssemblyExtensions")]
        public void MemberSelector_Should_ReturnSelector()
        {
            var actual = Assembly.GetExecutingAssembly().GetAllInstanceTypes(typeof(IStub));

            actual.Should().HaveCount(2);
            actual.Should().Contain(typeof(Stub))
                .And.Contain(typeof(DerivedStub))
                .And.NotContain(typeof(AbstractStub));
        }
    }
}