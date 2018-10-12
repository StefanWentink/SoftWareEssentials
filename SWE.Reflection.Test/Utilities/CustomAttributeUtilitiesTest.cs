namespace SWE.Reflection.Test
{
    using FluentAssertions;
    using global::Xunit;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using SWE.Reflection.Test.Data;
    using SWE.Reflection.Utilities;
    using SWE.Xunit.Attributes;
    using System.Linq;

    public class CustomAttributeUtilitiesTest
    {
        [Fact]
        [Category("CustomAttributeUtilities")]
        public void GetCustomAttribute_Should_ReturnCustomAttributes()
        {
            var actual = typeof(Stub).GetMethods().SelectMany(CustomAttributeUtilities.GetCustomAttribute);

            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.GetType() == typeof(HttpGetAttribute))
                .And.Contain(x => x.GetType() == typeof(HttpPostAttribute));
        }

        [Fact]
        [Category("CustomAttributeUtilities")]
        public void GetCustomAttribute_Should_ReturnCustomAttributes_When_Filtered()
        {
            var actual = typeof(Stub).GetMethods().SelectMany(x => CustomAttributeUtilities.GetCustomAttribute(x, new HttpGetAttribute()));

            actual.Should().HaveCount(1);
            actual.Should().Contain(x => x.GetType() == typeof(HttpGetAttribute))
                .And.NotContain(x => x.GetType() == typeof(HttpPostAttribute));
        }

        [Fact]
        [Category("CustomAttributeUtilities")]
        public void GetCustomAttribute_Should_ReturnCustomAttributes_When_FilteredOnDerived()
        {
            var actual = typeof(Stub).GetMethods().SelectMany(x => CustomAttributeUtilities.GetCustomAttribute(x, typeof(HttpMethodAttribute)));

            actual.Should().HaveCount(2);
            actual.Should().Contain(x => x.GetType() == typeof(HttpGetAttribute))
                .And.Contain(x => x.GetType() == typeof(HttpPostAttribute));
        }

        [Fact]
        [Category("CustomAttributeUtilities")]
        public void GetCustomAttribute_Should_ReturnNoCustomAttributes()
        {
            var actual = typeof(DerivedStub).GetMethods().SelectMany(x => CustomAttributeUtilities.GetCustomAttribute(x, new HttpPostAttribute()));

            actual.Should().BeEmpty();
        }

        [Fact]
        [Category("CustomAttributeUtilities")]
        public void GetCustomAttribute_Should_ReturnCustomDerivedAttributes_When_FilteredOnDerived()
        {
            var actual = typeof(DerivedStub).GetMethods().SelectMany(x => CustomAttributeUtilities.GetCustomAttribute(x, typeof(HttpMethodAttribute)));

            actual.Should().HaveCount(1);
            actual.Should().Contain(x => x.GetType() == typeof(HttpGetAttribute))
                .And.NotContain(x => x.GetType() == typeof(HttpPostAttribute));
        }
    }
}