namespace SWE.BasicType.Utilities
{
    using SWE.BasicType.Constants;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StandardDeviationUtilities
    {
        public static (double average, double standardDeviation) GetStandardDeviationAverage(List<double> values)
        {
            if (values.Count == 0)
            {
                return (0, 0);
            }

            var average = values.Average();

            return (average, Math.Sqrt(values.Sum(x => Math.Pow(x - average, 2)) / values.Count));
        }

        public static bool IsStandardDeviationValid(
            List<double> valueList,
            double standardDeviation)
        {
            return IsStandardDeviationValid(
                valueList,
                standardDeviation,
                StandardDeviationConstants.OneThresHold,
                StandardDeviationConstants.TwoThresHold,
                StandardDeviationConstants.ThreeThresHold);
        }

        public static bool IsStandardDeviationValid(
            List<double> valueList,
            double standardDeviation,
            double oneStandardDeviationPercentageThresHold,
            double twoStandardDeviationPercentageThresHold,
            double threeStandardDeviationPercentageThresHold)
        {
            var average = valueList.Average();
            var count = valueList.Count;

            return IsStandardDeviationValid(valueList, count, average, standardDeviation, 1, oneStandardDeviationPercentageThresHold)
                   && IsStandardDeviationValid(valueList, count, average, standardDeviation, 2, twoStandardDeviationPercentageThresHold)
                   && IsStandardDeviationValid(valueList, count, average, standardDeviation, 3, threeStandardDeviationPercentageThresHold);
        }

        internal static bool IsStandardDeviationValid(
            List<double> valueList,
            int count,
            double average,
            double standardDeviation,
            double standardDeviationFactor,
            double standardDeviationPercentageThresHold)
        {
            var lowerBound = average - standardDeviation * standardDeviationFactor;
            var upperBound = average + standardDeviation * standardDeviationFactor;
            var countBetweenBoundaries = valueList.Count(
                x =>
                    CompareUtilities.GreaterOrEqualTo(x, lowerBound) &&
                    CompareUtilities.SmallerOrEqualTo(x, upperBound));
            var percentageBetweenBoundaries = (double)countBetweenBoundaries / count * 100;
            return percentageBetweenBoundaries >= standardDeviationPercentageThresHold;
        }
    }
}