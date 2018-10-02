namespace SWE.Model.Test.Data
{
    using System;

    internal class NoWithStub : WithStub
    {
        public NoWithStub(int intProperty, int secondIntPropertyValue, double doubleProperty, string stringProperty, Guid guidProperty, DateTimeOffset dateTimeOffsetProperty)
            : base(intProperty, secondIntPropertyValue, doubleProperty, stringProperty, guidProperty, dateTimeOffsetProperty)
        {
        }
    }
}