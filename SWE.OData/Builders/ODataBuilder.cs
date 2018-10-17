namespace SWE.OData.Builders
{
    using SWE.BasicType.Utilities;
    using SWE.OData.Interfaces;
    using SWE.OData.Models;
    using SWE.Reflection.Extensions;
    using SWE.Reflection.Utilities;
    using System;
    using System.Linq.Expressions;
    using System.Text;

    public class ODataBuilder<T, TKey> : ODataKeyBuilder<T, TKey>, IODataBuilder<T, TKey>
        where TKey : IComparable<TKey>
    {
        public ODataBuilder()
            : base(typeof(T).Name)
        {
        }

        public ODataBuilder(string entity)
            : base(entity)
        {
        }

        public ODataBuilder(TKey key)
            : base(typeof(T).Name, key)
        {
        }

        public ODataBuilder(string entity, TKey key)
            : base(entity, key)
        {
        }

        protected IODataEntityBuilder SubEntity { get; set; }

        public ODataBuilder<T, TKey> Expand(IODataEntityBuilder subEntity)
        {
            subEntity.SubBuilder = true;
            SubEntity = subEntity;
            return this;
        }

        protected IODataFilters Filter { get; set; }

        public ODataBuilder<T, TKey> SetFilter(IODataFilters filters)
        {
            Filter = filters;
            return this;
        }

        public ODataBuilder<T, TKey> SetFilter(IODataFilter filter)
        {
            Filter = new ODataFilters(filter);
            return this;
        }

        protected string Order { get; set; }

        public ODataBuilder<T, TKey> SetOrder(string order)
        {
            Order = order;
            return this;
        }

        public ODataBuilder<T, TKey> SetOrder<TOrder, TValue>(Expression<Func<TOrder, TValue>> order)
        {
            Order = order.GetMemberInfo().Member.Name;
            return this;
        }

        protected bool Descending { get; set; }

        public ODataBuilder<T, TKey> SetDescending(bool descending)
        {
            Descending = descending;
            return this;
        }

        protected int Top { get; set; }

        public ODataBuilder<T, TKey> SetTop(int top)
        {
            Top = top;
            return this;
        }

        protected int Skip { get; set; }

        public ODataBuilder<T, TKey> SetSkip(int skip)
        {
            Skip = skip;
            return this;
        }

        protected override string BuildResult()
        {
            return BuildResult(false);
        }

        public override string BuilderKey()
        {
            return BuildResult(true);
        }

        protected string BuildResult(bool key)
        {
            var result = new StringBuilder();
            var prefix = "?";

            if (SubBuilder)
            {
                result.Append("$expand=");
                prefix = "&";
            }

            result.Append(base.BuildResult());

            var options = BuildOptionsResult(prefix, key);

            if (!string.IsNullOrWhiteSpace(options))
            {
                if (!CompareUtilities.IsDefault(Key))
                {
                    throw new ArgumentException($"{Filter} may only be used on collections. {nameof(Key)} is already applied.");
                }

                if (SubBuilder)
                {
                    result.Append("(");
                }

                result.Append(options);

                if (SubBuilder)
                {
                    result.Append(")");
                }

                prefix = "&";
            }

            if (SubEntity != null)
            {
                result.Append(prefix);
                result.Append(SubEntity.Build());
            }

            return result.ToString();
        }

        protected string BuildOptionsResult(string prefix, bool key)
        {
            var result = new StringBuilder();
            var actualPrefix = SubBuilder ? string.Empty : prefix;
            var replacementPrefix = SubBuilder ? ";" : "&";

            if (Filter != null)
            {
                Apply(result, "filter", Filter.ToString(), actualPrefix);
                actualPrefix = replacementPrefix;
            }

            if (!string.IsNullOrWhiteSpace(Order))
            {
                Apply(result, "orderby", ReflectionUtilities.GetPropertyNameFromPath(Order).ToLowerInvariant(), actualPrefix, Descending ? "desc" : string.Empty);
                actualPrefix = replacementPrefix;
            }

            if (Top != default && !key)
            {
                Apply(result, "top", Top.ToString(), actualPrefix);
                actualPrefix = replacementPrefix;
            }

            if (Skip != default && !key)
            {
                Apply(result, "skip", Skip.ToString(), actualPrefix);
                actualPrefix = replacementPrefix;
            }

            return result.ToString();
        }

        private void Apply(StringBuilder stringBuilder, string key, string value, string prefix)
        {
            Apply(stringBuilder, key, value, prefix, null);
        }

        private void Apply(StringBuilder stringBuilder, string key, string value, string prefix, string suffix)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                stringBuilder.Append(prefix);
            }

            stringBuilder
                .Append("$")
                .Append(key)
                .Append("=").Append(value);

            if (!string.IsNullOrWhiteSpace(suffix))
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(suffix);
            }
        }
    }
}