namespace SWE.OData.Enums
{
    using System.ComponentModel;

    public enum FilterOperator
    {
        [Description("unknown")]
        Unknown = 0,

        [Description("eq")]
        Equal = 1,

        [Description("ne")]
        NotEqual = 2,

        [Description("ge")]
        GreaterOrEquals = 3,

        [Description("gt")]
        GreaterThan = 4,

        [Description("le")]
        LessOrEquals = 5,

        [Description("lt")]
        LessThan = 6
    }
}