namespace SWE.BasicType.Extensions
{
    using System;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;
    using global::Xunit;
    using System.Linq;
    using SWE.Collection.Test.Data;

    public class CollectionExtensionsTest
    {
        [Fact]
        [Category("CollectionExtensions")]
        public void SplitInBatches_Should_ThrowArgumentException_When_MaxBatchNumberSmallerOrEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => new List<int>().SplitInBatches(0));
            Assert.Throws<ArgumentException>(() => new List<int>().SplitInBatches(-1));
        }

        [Theory]
        [Category("CollectionExtensions")]
        [InlineData(100, 10, 10, 10)]
        [InlineData(99, 10, 10, 9)]
        [InlineData(101, 10, 11, 1)]
        [InlineData(0, 10, 0, 0)]
        [InlineData(9, 10, 1, 9)]
        [InlineData(10, 10, 1, 10)]
        [InlineData(11, 10, 2, 1)]
        public void SplitInBatches_Should_SplitInBatches_When_MaxBatchNumberSmallerOrEqualToZero(
            int size,
            int batchSize,
            int expectedBatchCount,
            int expectedFinalBatchSize)
        {
            var actual = Enumerable.Range(1234, size).SplitInBatches(batchSize).ToList();
            Assert.Equal(expectedBatchCount, actual.Count);
            Assert.Equal(expectedFinalBatchSize, actual.LastOrDefault()?.Count() ?? 0);
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void UpdateOrAddItem_Should_Insert_When_NoMatches()
        {
            var collection = CollectionFactory.GetDefaultList();
            var value = CollectionFactory.GetDefaultValue(CollectionFactory.StartingValue - 1);

            collection.UpdateOrAddItem(value, x => x.key == value.key, true, true);

            Assert.Equal(4, collection.Count);
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 1));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 2));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 3));
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void UpdateOrAddItem_Should_UpdateAndRemoveFirstMatch_When_MultipleMatches()
        {
            var collection = CollectionFactory.GetDefaultList();
            var value = CollectionFactory.GetDefaultValue(CollectionFactory.StartingValue);

            collection.UpdateOrAddItem(value, x => x.key == value.key, false, false);

            Assert.Equal(3, collection.Count);
            Assert.Empty(collection.Where(x => x.key == CollectionFactory.StartingValue));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 1));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 2));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 3));
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void UpdateOrAddItem_Should_UpdateFirstMatchAndRemoveAllMatches_When_MultipleMatches()
        {
            var collection = CollectionFactory.GetDefaultList();
            var value = CollectionFactory.GetDefaultValue(CollectionFactory.StartingValue);

            collection.UpdateOrAddItem(value, x => x.key == value.key, true, false);

            Assert.Equal(2, collection.Count);
            Assert.Empty(collection.Where(x => x.key == CollectionFactory.StartingValue));
            Assert.Empty(collection.Where(x => x.key == CollectionFactory.StartingValue + 1));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 2));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 3));
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void UpdateOrAddItem_Should_UpdateAllMatches_When_MultipleMatches()
        {
            var collection = CollectionFactory.GetDefaultList();
            var value = CollectionFactory.GetDefaultValue(CollectionFactory.StartingValue);

            collection.UpdateOrAddItem(value, x => x.key == value.key, true, true);

            Assert.Equal(3, collection.Count);
            Assert.Empty(collection.Where(x => x.key == CollectionFactory.StartingValue));
            Assert.Empty(collection.Where(x => x.key == CollectionFactory.StartingValue + 1));
            Assert.Single(collection.Where(x => x.key == CollectionFactory.StartingValue + 2));
            Assert.Equal(2, collection.Count(x => x.key == CollectionFactory.StartingValue + 3));
        }

        [Theory]
        [Category("CollectionUtilities")]
        [InlineData(null)]
        [InlineData(default(List<string>))]
        public void IsNullOrEmpty_Should_ReturnTrue_WhenNullOrEmpty(List<string> values)
        {
            Assert.True(values.IsNullOrEmpty());
        }

        [Fact]
        [Category("CollectionUtilities")]
        public void IsNullOrEmpty_Should_ReturnFalse_WhenNotNullOrEmpty()
        {
            Assert.False(new List<string> { "" }.IsNullOrEmpty());
        }
    }
}