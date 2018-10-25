namespace SWE.AnomalyAnalysis.Models.Calculators
{
    using SWE.AnomalyAnalysis.Interfaces;
    using SWE.BasicType.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class StandardDeviationCalculator : ICalculator<double>
    {
        private double Factor { get; }

        public StandardDeviationCalculator(double rangeFactor)
        {
            Factor = rangeFactor;
        }

        public IDetectionRange<double> Calculate(IEnumerable<double> values, IEnumerable<double> anomalies)
        {
            var calculationResult = StandardDeviationUtilities.GetStandardDeviationAverage(values.ToList());

            return new DetectionRange<double>(
                calculationResult.average - (calculationResult.standardDeviation * Factor),
                calculationResult.average - calculationResult.standardDeviation,
                calculationResult.average,
                calculationResult.average + calculationResult.standardDeviation,
                calculationResult.average + (calculationResult.standardDeviation * Factor));
        }
    }
}