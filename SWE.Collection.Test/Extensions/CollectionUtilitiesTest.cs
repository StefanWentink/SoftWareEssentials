namespace SWE.BasicType.Extensions
{
    using System;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;
    using global::Xunit;
    using System.Linq;

    public class CollectionUtilitiesTest
    {
        [Fact]
        [Category("CollectionUtilities")]
        public void CalculateTakeByRecordCount_Should_ReturnTake_WhenRecordCountNegative()
        {
            Assert.Equal(1, Utilities.CollectionUtilities.CalculateTakeByRecordCount(0, 1, -1));
        }

        [Fact]
        [Category("CollectionUtilities")]
        public void CalculateTakeByRecordCount_Should_Return0_WhenRecordCount0()
        {
            Assert.Equal(0, Utilities.CollectionUtilities.CalculateTakeByRecordCount(0, 1, 0));
        }

        [Theory]
        [Category("CollectionUtilities")]
        [InlineData(90, 10, 99, 9)]
        [InlineData(80, 10, 99, 10)]
        [InlineData(100, 10, 99, 0)]
        public void CalculateTakeByRecordCount_Should_ReturnAccountingRecordCount_WhenRecordProvided(
            int skip,
            int take,
            int recordCount,
            int expected)
        {
            Assert.Equal(expected, Utilities.CollectionUtilities.CalculateTakeByRecordCount(skip, take, recordCount));
        }

        [Theory]
        [Category("CollectionUtilities")]
        [InlineData(90, 10, 9, 9)]
        [InlineData(80, 10, 10, 10)]
        [InlineData(80, 10, 11, 10)]
        [InlineData(100, 10, 0, 10)]
        public void CalculateTakeByMaxTake_ShouldReturnMaxTake_WhenExceededByTake(
            int skip,
            int take,
            int maxTake,
            int expected)
        {
            Assert.Equal(expected, Utilities.CollectionUtilities.CalculateTakeByMaxTake(skip, take, maxTake));
        }
    }
}