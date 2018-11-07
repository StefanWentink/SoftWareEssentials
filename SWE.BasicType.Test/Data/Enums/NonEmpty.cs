using System.ComponentModel;

namespace SWE.BasicType.Test.Data.Enums
{
    public enum NonEmpty
    {
        Unknown = 0,

        [Description("one")]
        FirstValue = 1,

        SecondValue = 2,

        [Description("Three")]
        ThirdValue = 3
    }
}