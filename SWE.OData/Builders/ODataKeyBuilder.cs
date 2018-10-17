namespace SWE.OData.Builders
{
    using SWE.BasicType.Utilities;
    using SWE.OData.Interfaces;
    using SWE.OData.Utilities;
    using System;
    using System.Text;

    public class ODataKeyBuilder<T, TKey> : ODataEntityBuilder, IODataKeyBuilder<T, TKey>
        where TKey : IComparable<TKey>
    {
        public TKey Key { get; protected set; }

        public string StringKey => ODataValueUtilities.ParameterToString(Key, true);

        public ODataKeyBuilder(string entity)
            : base(entity)
        {
        }

        public ODataKeyBuilder(string entity, TKey key)
            : base(entity)
        {
            Key = key;
        }

        public ODataKeyBuilder<T, TKey> SetKey(TKey key)
        {
            Key = key;
            return this;
        }

        protected override string BuildResult()
        {
            var result = new StringBuilder();

            result.Append(Entity.ToLowerInvariant());

            if (!CompareUtilities.IsDefault(Key))
            {
                if (SubBuilder)
                {
                    throw new ArgumentException($"Expand can not be used with {nameof(Key)}.");
                }

                result
                    .Append("(")
                    .Append(StringKey)
                    .Append(")");
            }

            return result.ToString();
        }
    }
}