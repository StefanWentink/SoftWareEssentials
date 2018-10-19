namespace SWE.OData.Models
{
    using SWE.BasicType.Extensions;
    using SWE.OData.Enums;
    using SWE.OData.Interfaces;
    using SWE.OData.Utilities;
    using SWE.Reflection.Utilities;
    using System.Text;

    public class ODataFilter<TValue> : IODataFilter
    {
        public string Entity { get; protected set; }

        public string Property { get; protected set; }

        public FilterOperator Operator { get; protected set; }

        public TValue Value { get; protected set; }

        public string StringValue => ODataValueUtilities.ParameterToString(Value);

        public ODataFilter(string property, FilterOperator @operator, TValue value)
            : this(property, @operator, value, string.Empty)
        {
        }

        public ODataFilter(string property, FilterOperator @operator, TValue value, string entity)
        {
            Property = property;
            Operator = @operator;
            Value = value;
            Entity = entity;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Property))
            {
                if (!string.IsNullOrWhiteSpace(Entity))
                {
                    result.Append(ReflectionUtilities.GetPropertyNameFromPath(Entity).ToLowerInvariant());
                    result.Append("/");
                }

                result
                    .Append(ReflectionUtilities.GetPropertyNameFromPath(Property).ToLowerInvariant())
                    .Append(" ")
                    .Append(Operator.GetEnumDescription())
                    .Append(" ")
                    .Append(StringValue);
            }

            return result.ToString();
        }
    }
}