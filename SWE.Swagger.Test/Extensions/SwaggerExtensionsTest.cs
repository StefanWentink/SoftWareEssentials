namespace SWE.Swagger.Test.Extensions
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using SWE.Swagger.Extensions;
    using SWE.Swagger.Test.Data;

    public class SwaggerExtensionsTest
    {
        [Fact]
        [Category("SwaggerExtensions")]
        public void GetPath_Should_ReturnFullGetMethod()
        {
            var controllerType = typeof(StubController);
            var actual = SwaggerExtensions.GetPath("prefix", controllerType, controllerType.GetMethod(nameof(StubController.Get)));

            const string expected = "/prefix/Stub/Get(System.Guid id, System.Boolean single)";
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("SwaggerExtensions")]
        public void GetPath_Should_ReturnClearMethod()
        {
            var controllerType = typeof(StubController);
            var actual = SwaggerExtensions.GetPath("prefix", controllerType, controllerType.GetMethod(nameof(StubController.Clear)));

            const string expected = "/prefix/Stub/Clear()";
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("SwaggerExtensions")]
        public void GetPath_Should_ReturnFullDeleteMethod()
        {
            var controllerType = typeof(StubController);
            var actual = SwaggerExtensions.GetPath("prefix", controllerType, controllerType.GetMethod(nameof(StubController.Delete)));

            const string expected = "/prefix/Stub/Delete(System.Guid id)";
            Assert.Equal(expected, actual);
        }
    }
}