namespace SWE.EventSourcing.Factories
{
    using SWE.EventSourcing.Events.Change;
    using SWE.EventSourcing.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ChangeFactory
    {
        public static IEnumerable<ChangeEvent<T>> ToChangeEventExpression<T, TC, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Expression<Func<TC, TValue>> changeSelector)
        {
            var changeFunc = changeSelector.Compile();

            return ToChangeEvent(
                propertySelector,
                defaultValue,
                changes,
                changeFunc);
        }

        public static IEnumerable<ChangeEvent<T>> ToChangeEvent<T, TC, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Func<TC, TValue> changeSelector)
        {
            var previousValue = defaultValue;

            foreach (var change in changes)
            {
                var value = changeSelector(change);
                yield return new ChangeEvent<T>(new PropertyChange<T, TValue>(previousValue, value, propertySelector));
                previousValue = value;
            }
        }

        public static IEnumerable<OrderedChangeEvent<T, TOrder>> ToOrderedChangeEventExpression<T, TC, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Expression<Func<TC, TValue>> changeSelector,
            Expression<Func<TC, TOrder>> orderSelector)
            where TOrder : IComparable<TOrder>
        {
            var changeFunc = changeSelector.Compile();
            var orderFunc = orderSelector.Compile();

            return ToOrderedChangeEvent(
                propertySelector,
                defaultValue,
                changes,
                changeFunc,
                orderFunc);
        }

            public static IEnumerable<OrderedChangeEvent<T, TOrder>> ToOrderedChangeEvent<T, TC, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Func<TC, TValue> changeSelector,
            Func<TC, TOrder> orderSelector)
            where TOrder : IComparable<TOrder>
        {
            var previousValue = defaultValue;

            foreach (var change in changes)
            {
                var value = changeSelector(change);
                var order = orderSelector(change);
                yield return new OrderedChangeEvent<T, TOrder>(new PropertyChange<T, TValue>(previousValue, value, propertySelector), order);
                previousValue = value;
            }
        }
    }
}