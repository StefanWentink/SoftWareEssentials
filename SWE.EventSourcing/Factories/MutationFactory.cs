namespace SWE.EventSourcing.Factories
{
    using SWE.EventSourcing.Events.Mutation;
    using SWE.EventSourcing.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class MutationFactory
    {
        public static IEnumerable<MutationEvent<T>> ToMutationEventExpression<T, TC, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Expression<Func<TC, TValue>> mutationSelector)
        {
            var mutationFunc = mutationSelector.Compile();

            return ToMutationEvent(
                propertySelector,
                mutations,
                mutationFunc);
        }

        public static IEnumerable<MutationEvent<T>> ToMutationEvent<T, TC, TValue>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Func<TC, TValue> mutationSelector)
        {
            foreach (var mutation in mutations)
            {
                var value = mutationSelector(mutation);
                yield return new MutationEvent<T>(new PropertyMutation<T, TValue>(value, propertySelector));
            }
        }

        public static IEnumerable<OrderedMutationEvent<T, TOrder>> ToOrderedMutationEventExpression<T, TC, TValue, TOrder>(
            Expression<Func<T, TValue>> propertySelector,
            IEnumerable<TC> mutations,
            Expression<Func<TC, TValue>> mutationSelector,
            Expression<Func<TC, TOrder>> orderSelector)
            where TOrder : IComparable<TOrder>
        {
            var mutationFunc = mutationSelector.Compile();
            var orderFunc = orderSelector.Compile();

            return ToOrderedMutationEvent(
                propertySelector,
                mutations,
                mutationFunc,
                orderFunc);
        }

        public static IEnumerable<OrderedMutationEvent<T, TOrder>> ToOrderedMutationEvent<T, TC, TValue, TOrder>(
        Expression<Func<T, TValue>> propertySelector,
        IEnumerable<TC> mutations,
        Func<TC, TValue> mutationSelector,
        Func<TC, TOrder> orderSelector)
        where TOrder : IComparable<TOrder>
        {
            foreach (var mutation in mutations)
            {
                var value = mutationSelector(mutation);
                var order = orderSelector(mutation);
                yield return new OrderedMutationEvent<T, TOrder>(new PropertyMutation<T, TValue>(value, propertySelector), order);
            }
        }
    }
}