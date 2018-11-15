namespace SWE.EventSourcing.Extensions
{
    using MoreLinq;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using System.Collections.Generic;

    public static class EventExtensions
    {
        /// <summary>
        /// Applies <see cref="IEvent{T, TKey}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="event"></param>
        /// <param name="value"></param>
        public static void Apply<T, TKey>(this IEvent<T, TKey> @event, T value)
            where TKey : IEquatable<TKey>
        {
            @event.PropertyActions.ForEach(x => x.GetApplyValueAction()(value));
        }

        /// <summary>
        /// Reverts <see cref="IEvent{T, TKey}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="event"></param>
        /// <param name="value"></param>
        public static void Revert<T, TKey>(this IEvent<T, TKey> @event, T value)
            where TKey : IEquatable<TKey>
        {
            @event.PropertyActions.ForEach(x => x.GetRevertValueAction()(value));
        }

        /// <summary>
        /// Applies <see cref="IEnumerable{IEvent{T, TKey}}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="events"></param>
        /// <param name="value"></param>
        public static void Apply<T, TKey>(this IEnumerable<IEvent<T, TKey>> @events, T value)
            where TKey : IEquatable<TKey>
        {
            @events.ForEach(x => x.Apply(value));
        }

        /// <summary>
        /// Reverts <see cref="IEnumerable{IEvent{T, TKey}}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="events"></param>
        /// <param name="value"></param>
        public static void Revert<T, TKey>(this IEnumerable<IEvent<T, TKey>> @events, T value)
            where TKey : IEquatable<TKey>
        {
            @events.ForEach(x => x.Revert(value));
        }
    }
}