namespace SWE.Model.Test.Data
{
    using System;

    internal static class StubFactory
    {
        internal static readonly int IntPropertyValue = 23;

        internal static readonly int SecondIntPropertyValue = 50;

        internal static readonly double DoublePropertyValue = 34.5;

        internal static readonly string StringPropertyValue = "Original";

        internal static readonly Guid GuidPropertyValue = Guid.NewGuid();

        internal static readonly DateTimeOffset DateTimeOffsetPropertyValue = new DateTimeOffset(2018, 1, 2, 3, 4, 5, TimeSpan.FromHours(6));

        internal static WithStub GetWithStub()
        {
            return new WithStub(IntPropertyValue, SecondIntPropertyValue, DoublePropertyValue, StringPropertyValue, GuidPropertyValue, DateTimeOffsetPropertyValue);
        }

        internal static NoWithStub GetNoWithStub()
        {
            return new NoWithStub(IntPropertyValue, SecondIntPropertyValue, DoublePropertyValue, StringPropertyValue, GuidPropertyValue, DateTimeOffsetPropertyValue);
        }
    }
}