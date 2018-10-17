namespace SWE.OData.Enums
{
    using System.ComponentModel;

    public enum QueryOperator
    {
        [Description("unknown")]
        Unknown = 0,

        [Description("and")]
        And = 1,

        [Description("or")]
        Or = 2
    }
}