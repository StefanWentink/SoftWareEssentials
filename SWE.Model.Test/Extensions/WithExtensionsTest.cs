namespace SWE.Model.Test.Extensions
{
    using System;

    using global::Xunit;

    using SWE.Model.Extensions;
    using SWE.Model.Test.Data;

    public class WithExtensionsTest
    {
        [Fact]
        public void With_Should_GenerateCopyOfStub()
        {
            var original = StubFactory.GetWithStub();
            var with = original.With();

            Assert.IsType<WithStub>(original);
            Assert.IsType<WithStub>(with);

            Assert.Equal(original.IntProperty, with.IntProperty);
            Assert.Equal(original.DoubleProperty, with.DoubleProperty);
            Assert.Equal(original.StringProperty, with.StringProperty);
            Assert.Equal(original.GuidProperty, with.GuidProperty);
            Assert.Equal(original.DateTimeOffsetProperty, with.DateTimeOffsetProperty);

            with.IntProperty += 1;
            with.DoubleProperty += 1;
            with.StringProperty = with.StringProperty.ToUpper();

            original.GuidProperty = Guid.NewGuid();
            original.DateTimeOffsetProperty = original.DateTimeOffsetProperty.AddSeconds(1);

            Assert.NotEqual(original.IntProperty, with.IntProperty);
            Assert.NotEqual(original.DoubleProperty, with.DoubleProperty);
            Assert.NotEqual(original.StringProperty, with.StringProperty);
            Assert.NotEqual(original.GuidProperty, with.GuidProperty);
            Assert.NotEqual(original.DateTimeOffsetProperty, with.DateTimeOffsetProperty);
        }

        [Fact]
        public void NoWith_Should_GenerateCopyOfStub()
        {
            var original = StubFactory.GetNoWithStub();
            var with = original.With();

            Assert.IsType<NoWithStub>(original);
            Assert.IsType<WithStub>(with);

            Assert.Equal(original.IntProperty, with.IntProperty);
            Assert.Equal(original.DoubleProperty, with.DoubleProperty);
            Assert.Equal(original.StringProperty, with.StringProperty);
            Assert.Equal(original.GuidProperty, with.GuidProperty);
            Assert.Equal(original.DateTimeOffsetProperty, with.DateTimeOffsetProperty);
        }

        [Fact]
        public void WithAction_Should_GenerateCopyOfStub_WithAction()
        {
            void SetAction(WithStub stub)
            {
                stub.IntProperty = StubFactory.IntPropertyValue + 1;
            }

            var original = StubFactory.GetNoWithStub();
            var with = original.With((Action<WithStub>)SetAction);

            Assert.IsType<NoWithStub>(original);
            Assert.IsType<WithStub>(with);

            Assert.NotEqual(original.IntProperty, with.IntProperty);
            Assert.Equal(original.DoubleProperty, with.DoubleProperty);
            Assert.Equal(original.StringProperty, with.StringProperty);
            Assert.Equal(original.GuidProperty, with.GuidProperty);
            Assert.Equal(original.DateTimeOffsetProperty, with.DateTimeOffsetProperty);
        }
    }
}