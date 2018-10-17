namespace SWE.OData.Interfaces
{
    using SWE.OData.Enums;

    public interface IODataFilter
    {
        string Entity { get; }

        string Property { get; }

        FilterOperator Operator { get; }

        string StringValue { get; }
    }
}