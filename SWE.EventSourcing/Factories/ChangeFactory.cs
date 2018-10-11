namespace SWE.EventSourcing.Factories
{
    using SWE.EventSourcing.Events.Change;
    using SWE.EventSourcing.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class ChangeFactory
    {
        public static IEnumerable<ChangeEvent<T, TKey>> ToChangeEventExpression<T, TC, TKey, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Expression<Func<TC, TKey>> keySelector,
            Expression<Func<TC, TValue>> changeSelector)
            where TKey : IEquatable<TKey>
        {
            var keyFunc = keySelector.Compile();
            var changeFunc = changeSelector.Compile();

            return ToChangeEvent(
                propertySelector,
                defaultValue,
                changes,
                keyFunc,
                changeFunc);
        }

        public static IEnumerable<ChangeEvent<T, TKey>> ToChangeEvent<T, TC, TKey, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Func<TC, TKey> keySelector,
            Func<TC, TValue> changeSelector)
            where TKey : IEquatable<TKey>
        {
            var previousValue = defaultValue;

            foreach (var change in changes)
            {
                var value = changeSelector(change);
                yield return new ChangeEvent<T, TKey>(
                    keySelector(change),
                    new PropertyChange<T, TValue>(previousValue, value, propertySelector));
                previousValue = value;
            }
        }

        public static IEnumerable<OrderedChangeEvent<T, TKey, TOrder>> ToOrderedChangeEventExpression<T, TC, TKey, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Expression<Func<TC, TKey>> keySelector,
            Expression<Func<TC, TValue>> changeSelector,
            Expression<Func<TC, TOrder>> orderSelector)
            where TKey : IEquatable<TKey>
            where TOrder : IComparable<TOrder>
        {
            var keyFunc = keySelector.Compile();
            var changeFunc = changeSelector.Compile();
            var orderFunc = orderSelector.Compile();

            return ToOrderedChangeEvent(
                propertySelector,
                defaultValue,
                changes,
                keyFunc,
                changeFunc,
                orderFunc);
        }

        public static IEnumerable<OrderedChangeEvent<T, TKey, TOrder>> ToOrderedChangeEvent<T, TC, TKey, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            TValue defaultValue,
            IEnumerable<TC> changes,
            Func<TC, TKey> keySelector,
            Func<TC, TValue> changeSelector,
            Func<TC, TOrder> orderSelector)
            where TKey : IEquatable<TKey>
            where TOrder : IComparable<TOrder>
        {
            var previousValue = defaultValue;

            foreach (var change in changes)
            {
                var value = changeSelector(change);
                var order = orderSelector(change);
                yield return new OrderedChangeEvent<T, TKey, TOrder>(
                    keySelector(change),
                    new PropertyChange<T, TValue>(previousValue, value, propertySelector),
                    order);
                previousValue = value;
            }
        }
    }
}