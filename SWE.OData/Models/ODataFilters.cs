namespace SWE.OData.Models
{
    using SWE.OData.Enums;
    using SWE.OData.Interfaces;
    using System;
    using System.Text;
    using SWE.BasicType.Extensions;
    using System.Collections.Generic;

    public class ODataFilters : IODataFilters
    {
        public QueryOperator Operator { get; set; } = QueryOperator.And;

        public List<IODataFilter> FilterCollection { get; set; } = new List<IODataFilter>();

        public List<IODataFilters> FiltersCollection { get; set; } = new List<IODataFilters>();

        [Obsolete("Only for serialization.", true)]
        public ODataFilters()
        { }

        public ODataFilters(IODataFilter filter)
            : this(new List<IODataFilter> { filter })
        {
        }

        public ODataFilters(List<IODataFilter> filterCollection)
        {
            FilterCollection = filterCollection;
        }

        public ODataFilters(IODataFilters filters)
            : this(new List<IODataFilters> { filters })
        {
        }

        public ODataFilters(List<IODataFilters> filtersCollection)
        {
            FiltersCollection = filtersCollection;
        }

        public ODataFilters(
            IODataFilter filter,
            IODataFilters filters)
            : this(new List<IODataFilter> { filter }, filters, QueryOperator.And)
        {
        }

        public ODataFilters(
            IODataFilter filter,
            IODataFilters filters,
            QueryOperator @operator)
            : this(new List<IODataFilter> { filter }, filters, @operator)
        {
        }

        public ODataFilters(
            IODataFilter filter,
            List<IODataFilters> filtersCollection)
            : this(new List<IODataFilter> { filter }, filtersCollection, QueryOperator.And)
        {
        }

        public ODataFilters(
            IODataFilter filter,
            List<IODataFilters> filtersCollection,
            QueryOperator @operator)
            : this(new List<IODataFilter> { filter }, filtersCollection, @operator)
        {
        }

        public ODataFilters(
            List<IODataFilter> filterCollection,
            IODataFilters filters)
            : this(filterCollection, filters, QueryOperator.And)
        {
        }

        public ODataFilters(
            List<IODataFilter> filterCollection,
            IODataFilters filters,
            QueryOperator @operator)
            : this(filterCollection, new List<IODataFilters> { filters }, @operator)
        {
        }

        public ODataFilters(
            List<IODataFilter> filterCollection,
            List<IODataFilters> filtersCollection)
            : this(filterCollection, filtersCollection, QueryOperator.And)
        {
        }

        public ODataFilters(
            List<IODataFilter> filterCollection,
            List<IODataFilters> filtersCollection,
            QueryOperator @operator)
        {
            Operator = @operator;
            FilterCollection = filterCollection;
            FiltersCollection = filtersCollection;
        }

        public void AddFilter(IODataFilter filter)
        {
            FilterCollection.Add(filter);
        }

        public void AddFilter(List<IODataFilter> filterCollection)
        {
            FilterCollection.AddRange(filterCollection);
        }

        public void AddFilters(IODataFilters filters)
        {
            FiltersCollection.Add(filters);
        }

        public void AddFilters(List<IODataFilters> filtersCollection)
        {
            FiltersCollection.AddRange(filtersCollection);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            if (FilterCollection != null)
            {
                foreach (var filter in FilterCollection)
                {
                    ApplyOperator(result);
                    result.Append(filter.ToString());
                }
            }

            if (FiltersCollection != null)
            {
                foreach (var filters in FiltersCollection)
                {
                    ApplyOperator(result);
                    result.Append(filters.ToString());
                }
            }

            return result.ToString();
        }

        private void ApplyOperator(StringBuilder stringBuilder)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder
                    .Append(" ")
                    .Append(Operator.GetEnumDescription())
                    .Append(" ");
            }
        }
    }
}