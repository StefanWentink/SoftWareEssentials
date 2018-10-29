namespace SWE.BasicType.Test.Utilities
{
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;
    using global::Xunit;
    using System.Linq;
    using SWE.BasicType.Constants;

    public class StandardDeviationUtilitiesTest
    {
        [Fact]
        [Category("StandardDeviationUtilities")]
        public void GetStandardDeviationTest()
        {
            var values = new List<double> { 1000, 1100, 1130, 1140, 1145, 1165, 1200 };
            const double ExpectedStandardDeviation = 58.7019451751553;
            var expectedAverage = values.Average();

            var result = StandardDeviationUtilities.GetStandardDeviationAverage(values);
            var actualAverage = result.average;
            var actualStandardDeviation = result.standardDeviation;

            Assert.True(CompareUtilities.EqualsWithinTolerance(ExpectedStandardDeviation, actualStandardDeviation, 6));
            Assert.True(CompareUtilities.EqualsWithinTolerance(expectedAverage, actualAverage, 6));
        }

        [Theory]
        [Category("StandardDeviationUtilities")]
        [InlineData(true, true, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void IsStandardDeviationValidTest(bool expectedOne, bool expectedTwo, bool expectedThree)
        {
            const int baseValue = 1500;
            const int numberOfValuesOnePercent = 1000 / 100;

            var values = new List<double>();

            const double standardDeviation = numberOfValuesOnePercent * StandardDeviationConstants.OneThresHold / 2;
            for (var i = -standardDeviation + 1; i < standardDeviation - 1; i++)
            {
                // 681 values
                values.Add(baseValue + i);
            }

            const double deviationOne = numberOfValuesOnePercent * (StandardDeviationConstants.TwoThresHold - StandardDeviationConstants.OneThresHold);
            for (var i = expectedOne ? 2 : 1; i <= deviationOne + 1; i++)
            {
                // 270 values (or 1 extra)
                values.Add(baseValue + (i % 2 == 0 ? (standardDeviation + i) : -(standardDeviation + i)));
            }

            const double deviationTwo = numberOfValuesOnePercent * (StandardDeviationConstants.ThreeThresHold - StandardDeviationConstants.TwoThresHold);
            for (var i = expectedTwo ? 2 : 1; i <= deviationTwo + 1; i++)
            {
                // 47 values (or 1 extra)
                values.Add(baseValue + (i % 2 == 0 ? (standardDeviation * 2 + i) : -(standardDeviation * 2 + i)));
            }

            const double deviationThree = numberOfValuesOnePercent * (100 - StandardDeviationConstants.ThreeThresHold);
            for (var i = expectedThree ? 2 : 1; i <= deviationThree + 1; i++)
            {
                // 3 values (or 1 extra)
                values.Add(baseValue + (i % 2 == 0 ? (standardDeviation * 3 + i) : -(standardDeviation * 3 + i)));
            }

            var actual = StandardDeviationUtilities.IsStandardDeviationValid(values, standardDeviation);
            var expected = expectedOne && expectedTwo && expectedThree;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [Category("StandardDeviationUtilities")]
        [InlineData(true)]
        [InlineData(false)]
        public void IsStandardDeviationValidOneTest(bool expected)
        {
            var values = new List<double>();
            const double standardDeviation = 1;

            for (var i = (expected ? 0 : 1); i < 680; i++)
            {
                values.Add(10);
            }

            for (var i = (expected ? 680 : 679); i < 950; i++)
            {
                values.Add(11.1);
            }

            for (var i = 950; i < 997; i++)
            {
                values.Add(12.1);
            }

            for (var i = 997; i < 1000; i++)
            {
                values.Add(13.1);
            }

            const int count = 1000;
            const double average = 10;

            var actualOne = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 1, StandardDeviationConstants.OneThresHold);
            var actualTwo = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 2, StandardDeviationConstants.TwoThresHold);
            var actualThree = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 3, StandardDeviationConstants.ThreeThresHold);

            Assert.Equal(expected, actualOne);
            Assert.True(actualTwo);
            Assert.True(actualThree);
        }

        [Theory]
        [Category("StandardDeviationUtilities")]
        [InlineData(true)]
        [InlineData(false)]
        public void IsStandardDeviationValidTwoTest(bool expected)
        {
            var values = new List<double>();
            const double standardDeviation = 1;

            for (var i = 0; i < 680; i++)
            {
                values.Add(10);
            }

            for (var i = (expected ? 680 : 681); i < 950; i++)
            {
                values.Add(11.1);
            }

            for (var i = (expected ? 950 : 949); i < 997; i++)
            {
                values.Add(12.1);
            }

            for (var i = 997; i < 1000; i++)
            {
                values.Add(13.1);
            }

            const int count = 1000;
            const double average = 10;

            var actualOne = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 1, StandardDeviationConstants.OneThresHold);
            var actualTwo = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 2, StandardDeviationConstants.TwoThresHold);
            var actualThree = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 3, StandardDeviationConstants.ThreeThresHold);

            Assert.True(actualOne);
            Assert.Equal(expected, actualTwo);
            Assert.True(actualThree);
        }

        [Theory]
        [Category("StandardDeviationUtilities")]
        [InlineData(true)]
        [InlineData(false)]
        public void IsStandardDeviationValidThreeTest(bool expected)
        {
            var values = new List<double>();
            const double standardDeviation = 1;

            for (var i = 0; i < 680; i++)
            {
                values.Add(10);
            }

            for (var i = 680; i < 950; i++)
            {
                values.Add(11.1);
            }

            for (var i = (expected ? 950 : 951); i < 997; i++)
            {
                values.Add(12.1);
            }

            for (var i = (expected ? 997 : 996); i < 1000; i++)
            {
                values.Add(13.1);
            }

            const int count = 1000;
            const double average = 10;

            var actualOne = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 1, StandardDeviationConstants.OneThresHold);
            var actualTwo = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 2, StandardDeviationConstants.TwoThresHold);
            var actualThree = StandardDeviationUtilities.IsStandardDeviationValid(values, count, average, standardDeviation, 3, StandardDeviationConstants.ThreeThresHold);

            Assert.True(actualOne);
            Assert.True(actualTwo);
            Assert.Equal(expected, actualThree);
        }
    }
}