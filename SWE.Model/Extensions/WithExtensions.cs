namespace SWE.Model.Extensions
{
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;

    public static class WithExtensions
    {
        public static TOut With<TWith, TOut>(this TWith item, Action<TOut> setAction)
            where TWith : IWith<TOut>
        {
            return item.With(new[] { setAction });
        }

        public static TOut With<TWith, TOut>(this TWith item, IEnumerable<Action<TOut>> setActions)
            where TWith : IWith<TOut>
        {
            var result = item.With();

            foreach (var setAction in setActions)
            {
                setAction(result);
            }

            return result;
        }
    }
}