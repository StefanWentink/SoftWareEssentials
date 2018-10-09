namespace SWE.EventSourcing.Factories
{
    using SWE.EventSourcing.Events.Mutation;
    using SWE.EventSourcing.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class MutationFactory
    {
        public static IEnumerable<MutationEvent<T, TKey>> ToMutationEventExpression<T, TC, TKey, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Expression<Func<TC, TKey>> keySelector,
            Expression<Func<TC, TValue>> mutationSelector)
            where TKey : IEquatable<TKey>
        {
            var keyFunc = keySelector.Compile();
            var mutationFunc = mutationSelector.Compile();

            return ToMutationEvent(
                propertySelector,
                mutations,
                keyFunc,
                mutationFunc);
        }

        public static IEnumerable<MutationEvent<T, TKey>> ToMutationEvent<T, TC, TKey, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Func<TC, TKey> keySelector,
            Func<TC, TValue> mutationSelector)
            where TKey : IEquatable<TKey>
        {
            foreach (var mutation in mutations)
            {
                var value = mutationSelector(mutation);
                yield return new MutationEvent<T, TKey>(
                    keySelector(mutation),
                    new PropertyMutation<T, TValue>(value, propertySelector));
            }
        }

        public static IEnumerable<OrderedMutationEvent<T, TKey, TOrder>> ToOrderedMutationEventExpression<T, TC, TKey, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Expression<Func<TC, TKey>> keySelector,
            Expression<Func<TC, TValue>> mutationSelector,
            Expression<Func<TC, TOrder>> orderSelector)
            where TKey : IEquatable<TKey>
            where TOrder : IComparable<TOrder>
        {
            var keyFunc = keySelector.Compile();
            var mutationFunc = mutationSelector.Compile();
            var orderFunc = orderSelector.Compile();

            return ToOrderedMutationEvent(
                propertySelector,
                mutations,
                keyFunc,
                mutationFunc,
                orderFunc);
        }

        public static IEnumerable<OrderedMutationEvent<T, TKey, TOrder>> ToOrderedMutationEvent<T, TC, TKey, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Func<TC, TKey> keySelector,
            Func<TC, TValue> mutationSelector,
            Func<TC, TOrder> orderSelector)
            where TKey : IEquatable<TKey>
            where TOrder : IComparable<TOrder>
        {
            foreach (var mutation in mutations)
            {
                var value = mutationSelector(mutation);
                var order = orderSelector(mutation);
                yield return new OrderedMutationEvent<T, TKey, TOrder>(
                    keySelector(mutation),
                    new PropertyMutation<T, TValue>(value, propertySelector), order);
            }
        }
    }
}