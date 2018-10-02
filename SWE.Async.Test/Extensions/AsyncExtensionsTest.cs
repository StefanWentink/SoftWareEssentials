namespace SWE.Async.Test.Extensions
{
    using System;
    using System.Threading.Tasks;

    using global::Xunit;

    using SWE.Async.Extensions;
    using SWE.Xunit.Attributes;

    public class AsyncExtensionsTest
    {
        private readonly Func<int, Task<string>> _intToStringAsync = (x) => Task.FromResult(x.ToString());

        private readonly Func<string, Task<int>> _stringToIntAsync = (x) => Task.FromResult(x.Length);

        [Theory]
        [InlineData(13)]
        [InlineData(2)]
        [Category("AsyncExtensions")]
        public async Task Map_Should_MapToTask(int value)
        {
            var task = AsyncExtensions.Map(value);

            var actual = await task.ConfigureAwait(false);

            Assert.Equal(value, actual);
        }

        [Theory]
        [InlineData(13, 2)]
        [InlineData(2, 1)]
        [Category("AsyncExtensions")]
        public async Task MapAsync_Should_ChainAsync(int value, int expected)
        {
            var task = AsyncExtensions.Map(value)
                .MapAsync(_intToStringAsync)
                .MapAsync(_stringToIntAsync);

            var actual = await task.ConfigureAwait(false);

            Assert.Equal(expected, actual);
        }
    }
}
