namespace SWE.Model.Models
{
    using System;

    using SWE.Model.Interfaces;

    public class RangeModel<TValue> : IRange<TValue>
        where TValue : IComparable<TValue>
    {
        public TValue From { get; set; }

        public TValue Till { get; set; }

        [Obsolete("Only for serialization.", true)]
        public RangeModel()
        {
        }

        public RangeModel(TValue @from, TValue till)
        {
            From = @from;
            Till = till;
        }
    }
}