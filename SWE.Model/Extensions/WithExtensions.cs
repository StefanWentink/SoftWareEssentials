namespace SWE.Model.Extensions
{
    using System;
    using System.Collections.Generic;

    using SWE.Model.Interfaces;

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