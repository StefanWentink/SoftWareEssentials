using MoreLinq;
using SWE.EventSourcing.Interfaces.Events;
using System.Collections.Generic;

namespace SWE.EventSourcing.Extensions
{
    public static class EventExtensions
    {
        /// <summary>
        /// Applies <see cref="IEvent{T}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <param name="value"></param>
        public static void Apply<T>(this IEvent<T> @event, T value)
        {
            @event.PropertyActions.ForEach(x => x.GetApplyValueAction()(value));
        }

        /// <summary>
        /// Reverts <see cref="IEvent{T}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <param name="value"></param>
        public static void Revert<T>(this IEvent<T> @event, T value)
        {
            @event.PropertyActions.ForEach(x => x.GetRevertValueAction()(value));
        }

        /// <summary>
        /// Applies <see cref="IEnumerable{IEvent{T}}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events"></param>
        /// <param name="value"></param>
        public static void Apply<T>(this IEnumerable<IEvent<T>> @events, T value)
        {
            @events.ForEach(x => x.Apply(value));
        }

        /// <summary>
        /// Reverts <see cref="IEnumerable{IEvent{T}}"/> on <see cref="value"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events"></param>
        /// <param name="value"></param>
        public static void Revert<T>(this IEnumerable<IEvent<T>> @events, T value)
        {
            @events.ForEach(x => x.Revert(value));
        }
    }
}