namespace SWE.Collection.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        /// <summary>
        /// <see cref="IReadOnlyCollection{T}"/> are splitted in batches given by the <see cref="maxBatchNumber"/>.
        /// Do not use this for <see cref="string"/> which is also <see cref="IEnumerable"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <param name="maxBatchNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <see cref="maxBatchNumber"/> smaller or equal to 0.</exception>
        public static IEnumerable<IEnumerable<T>> SplitInBatches<T>(this IEnumerable<T> models, int maxBatchNumber)
        {
            if (maxBatchNumber <= 0)
            {
                throw new ArgumentException($"{nameof(maxBatchNumber)} must be greater than zero.");
            }

            return models.SplitInBatchesIterator(maxBatchNumber);
        }

        private static IEnumerable<IEnumerable<T>> SplitInBatchesIterator<T>(this IEnumerable<T> models, int maxBatchNumber)
        {
            var recordCount = models.Count();

            for (var skip = 0; skip < recordCount; skip += maxBatchNumber)
            {
                yield return models
                    .Skip(skip)
                    .Take(Utilities.CollectionUtilities.CalculateTakeByRecordCount(skip, maxBatchNumber, recordCount));
            }
        }

        /// <summary>
        /// Adds or updates an item to a list based on <see cref="compareFunction"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="compareFunction"></param>
        /// <param name="removeAllMatchingInstances">Remove all matching instances with <see cref="model"/> (Or replace just once).</param>
        /// <param name="replaceAllMatchingInstances">Replace all matching instances with <see cref="model"/> (Or replace just once).</param>
        public static void UpdateOrAddItem<T>(
            this List<T> list,
            T value,
            Func<T, bool> compareFunction,
            bool removeAllMatchingInstances,
            bool replaceAllMatchingInstances)
        {
            if (!list.UpdateItem(value,
                compareFunction,
                removeAllMatchingInstances,
                replaceAllMatchingInstances))
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Adds or updates an item to a list based on <see cref="compareFunction"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="compareFunction"></param>
        /// <param name="removeAllMatchingInstances">Remove all matching instances with <see cref="model"/> (Or replace just once).</param>
        /// <param name="replaceAllMatchingInstances">Replace all matching instances with <see cref="model"/> (Or replace just once).</param>
        public static bool UpdateItem<T>(
            this List<T> list,
            T value,
            Func<T, bool> compareFunction,
            bool removeAllMatchingInstances,
            bool replaceAllMatchingInstances)
        {
            var comparePredicate = new Predicate<T>(compareFunction);
            var index = list.FindIndex(comparePredicate);

            var replacementIndexes = new List<int>();

            while (index >= 0)
            {
                list.RemoveAt(index);

                if (!removeAllMatchingInstances)
                {
                    list.Insert(index, value);
                    return true;
                }

                if (replaceAllMatchingInstances || !replacementIndexes.Any())
                {
                    replacementIndexes.Add(index);
                }

                index = replaceAllMatchingInstances || removeAllMatchingInstances
                    ? list.FindIndex(index, comparePredicate)
                    : -1;
            }

            foreach (var replacementIndex in replacementIndexes.OrderByDescending(x => x))
            {
                list.Insert(replacementIndex, value);
            }

            return replacementIndexes.Any();
        }

        /// <summary>
        /// Validates wether the <see cref="value"/> is null or contains no elements.
        /// Do not use this for <see cref="string"/> which is also <see cref="IEnumerable"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T value)
            where T : IEnumerable
        {
            if (Equals(value, default(T)))
            {
                return true;
            }

            return !value.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Map to <see cref="IEnumerable{T}"/> ignoring <see cref="null"/>.
        /// Do not use this for <see cref="string"/> which is also <see cref="IEnumerable"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> MapToEnumerable<T>(this IEnumerable<T> value)
        {
            return value ?? new List<T>();
        }
    }
}