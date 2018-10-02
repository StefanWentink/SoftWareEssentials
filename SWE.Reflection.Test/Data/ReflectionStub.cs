namespace SWE.Reflection.Test.Data
{
    using System;

    internal class ReflectionStub
    {
        public int IntProperty { get; set; }

        public double DoubleProperty { get; set; }

        public string StringProperty { get; set; }

        public Guid GuidProperty { get; set; }

        public DateTimeOffset DateTimeOffsetProperty { get; set; }

        public ReflectionStub(int intProperty, double doubleProperty, string stringProperty, Guid guidProperty, DateTimeOffset dateTimeOffsetProperty)
        {
            IntProperty = intProperty;
            DoubleProperty = doubleProperty;
            StringProperty = stringProperty;
            GuidProperty = guidProperty;
            DateTimeOffsetProperty = dateTimeOffsetProperty;
        }
    }
} 