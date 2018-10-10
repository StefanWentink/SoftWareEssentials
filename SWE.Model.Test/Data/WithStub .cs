namespace SWE.Model.Test.Data
{
    using SWE.Model.Interfaces;
    using System;

    internal class WithStub : IWith<WithStub>
    {
        public int IntProperty { get; set; }

        public int SecondIntProperty { get; set; }

        public double DoubleProperty { get; set; }

        public string StringProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public DateTimeOffset DateTimeOffsetProperty { get; set; }

        public WithStub(int intProperty, int secondIntPropertyValue, double doubleProperty, string stringProperty, Guid guidProperty, DateTimeOffset dateTimeOffsetProperty)
        {
            IntProperty = intProperty;
            SecondIntProperty = secondIntPropertyValue;
            DoubleProperty = doubleProperty;
            StringProperty = stringProperty;
            GuidProperty = guidProperty;
            DateTimeOffsetProperty = dateTimeOffsetProperty;
        }

        public WithStub With()
        {
            return new WithStub(IntProperty, SecondIntProperty, DoubleProperty, StringProperty, GuidProperty, DateTimeOffsetProperty);
        }
    }
}