namespace SWE.OData.Interfaces
{
    using SWE.OData.Enums;
    using System.Collections.Generic;

    public interface IODataFilters
    {
        QueryOperator Operator { get; set; }

        List<IODataFilter> FilterCollection { get; set; }

        List<IODataFilters> FiltersCollection { get; set; }

        void AddFilter(IODataFilter filter);

        void AddFilter(List<IODataFilter> filterCollection);

        void AddFilters(IODataFilters filters);

        void AddFilters(List<IODataFilters> filtersCollection);
    }
}