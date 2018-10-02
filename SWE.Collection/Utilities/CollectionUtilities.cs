namespace SWE.BasicType.Utilities
{
    using System;
    using System.Collections;

    public static class CollectionUtilities
    {
        /// <summary>
        /// Calculates available and/or allowed take to prevent being out of range.
        /// </summary>
        /// <param name="skip">Requested skip.</param>
        /// <param name="take">Requested take.</param>
        /// <param name="recordCount">Number of records. Negative will not be evaluated. Parse negative if unkwown.</param>
        /// <returns>Calculated take.</returns>
        public static int CalculateTakeByRecordCount(int skip, int take, int recordCount)
        {
            return CalculateTake(skip, take, recordCount, 0);
        }

        /// <summary>
        /// Calculates available and/or allowed take to prevent being out of range.
        /// </summary>=
        /// <param name="skip">Requested skip.</param>
        /// <param name="take">Requested take.</param>
        /// <param name="maxTake">Will be ignored if 0.</param>
        /// <returns>Calculated take.</returns>
        public static int CalculateTakeByMaxTake(int skip, int take, int maxTake)
        {
            return CalculateTake(skip, take, -1, maxTake);
        }

        /// <summary>
        /// Calculates available and/or allowed take to prevent being out of range.
        /// </summary>
        /// <param name="skip">Requested skip.</param>
        /// <param name="take">Requested take.</param>
        /// <param name="recordCount">Number of records. Negative will not be evaluated. Parse negative if unkwown.</param>
        /// <param name="maximumTake">Will be ignored if 0.</param>
        /// <returns>Calculated take.</returns>
        public static int CalculateTake(int skip, int take, int recordCount, int maximumTake)
        {
            if (take < 0)
            {
                take = 0;
            }

            if (recordCount == 0)
            {
                return 0;
            }

            var result = take;

            if ((maximumTake > 0 && take > maximumTake) || result == 0)
            {
                result = maximumTake;
            }

            if (recordCount > 0)
            {
                if (skip >= recordCount)
                {
                    return 0;
                }

                if (skip + result > recordCount)
                {
                    result = Math.Max(recordCount - skip, 0);
                }
            }

            return result;
        }
    }
}