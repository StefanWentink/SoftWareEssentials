namespace SWE.BasicType.Utilities
{
    using System;

    public static class IntUtilities
    {
        /// <summary>
        /// Get maximum divisable int factor 
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <param name="remainder"> remainder of the devision </param>
        /// <returns>factor between numerator and denominator </returns>
        /// <exception cref="DivideByZeroException">When <see cref="denominator"/> is zero.</exception>
        public static int TryIntDivision(int numerator, int denominator, out int remainder)
        {
            if (CompareUtilities.IsDefault(denominator))
            {
                throw new DivideByZeroException(nameof(denominator));
            }

            remainder = numerator % denominator;
            return (int)Math.Floor((double)numerator / denominator);
        }
    }
}