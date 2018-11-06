namespace SWE.EntityFramework.Test
{
    using global::Xunit;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;
    using SWE.EntityFramework.Extensions;
    using System.Linq;
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;

    public class AsyncExtensionsTest
    {
        private List<string> list = new List<string> { "1", "2", "3" };

        [Fact]
        [Category("AsyncExtensions")]
        public async Task ToArrayAsyncSafe_Should_ResolveAsync()
        {
            (await list
                    .AsQueryable()
                    .ToArrayAsyncSafe()
                    .ConfigureAwait(false))
                .Count()
                .Should()
                .Be(list.Count);
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task ToListAsyncSafe_Should_ResolveAsync()
        {
            (await list
                    .AsQueryable()
                    .ToListAsyncSafe()
                    .ConfigureAwait(false))
                .Count()
                .Should()
                .Be(list.Count);
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task SingleOrDefaultAsyncSafe_Should_ResolveAsync()
        {
            (await list
                    .AsQueryable()
                    .Where(x => x == "2")
                    .SingleOrDefaultAsyncSafe()
                    .ConfigureAwait(false))
                .Should()
                .Be("2");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task SingleOrDefaultAsyncSafe_Should_ResolveAsync_With_Predicate()
        {
            (await list
                    .AsQueryable()
                    .SingleOrDefaultAsyncSafe(x => x == "2")
                    .ConfigureAwait(false))
                .Should()
                .Be("2");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task FirstOrDefaultAsyncSafe_Should_ResolveAsync()
        {
            (await list
                   .AsQueryable()
                   .FirstOrDefaultAsyncSafe()
                   .ConfigureAwait(false))
                .Should()
                .Be("1");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task FirstOrDefaultAsyncSafe_Should_ResolveAsync_With_Predicate()
        {
            (await list
                   .AsQueryable()
                   .FirstOrDefaultAsyncSafe(x => x != "1")
                   .ConfigureAwait(false))
                .Should()
                .Be("2");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task LastOrDefaultAsyncSafe_Should_ResolveAsync()
        {
            (await list
                   .AsQueryable()
                   .LastOrDefaultAsyncSafe()
                   .ConfigureAwait(false))
                .Should()
                .Be("3");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public async Task LastOrDefaultAsyncSafe_Should_ResolveAsync_With_Predicate()
        {
            (await list
                   .AsQueryable()
                   .LastOrDefaultAsyncSafe(x => x != "3")
                   .ConfigureAwait(false))
                .Should()
                .Be("2");
        }

        [Fact]
        [Category("AsyncExtensions")]
        public void ExceptionIfNullshould_Should_ThrowException_When_Null()
        {
            Action action = () => AsyncExtensions.ExceptionIfNull<string>(null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}