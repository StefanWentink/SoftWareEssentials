namespace Swe.Monad.Test.Models
{
    using global::Xunit;
    using Swe.Monad.Test.Data;
    using SWE.Monad.Utilities;
    using SWE.Xunit.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TryTests
    {
        [Theory]
        [Category("Try")]
        [InlineData(12, false)]
        [InlineData(13, true)]
        public void Try_Should_RecoverFromArgumentException(int number, bool equal)
        {
            const string expected = "Customer1";

            var actual = MonadUtilities
                    .ReturnTry(number)
                    .Map(x => x - 12)
                    .Map(x => new AddressStub("Some street", x))
                    .Map(x => new CustomerStub(expected, x))
                    .Recover<ArgumentException>(x => new CustomerStub(x.Message))
                    .Map(x => x.Name)
                    .GetValue();

            if (equal)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                Assert.NotEqual(expected, actual);
            }
        }

        [Fact]
        [Category("Try")]
        public void Try_Should_RecoverFromInvalidOperationException()
        {
            const int expected = 16;

            var actual = MonadUtilities
                    .ReturnTry(new AddressStub("Some street", 13))
                    .Map(x => new CustomerStub("Some customer", x))
                    .Map(x => x.Addresses.ToList())
                    .Recover<ArgumentException>(x => new List<AddressStub> { new AddressStub(x.Message, 14, true) })
                    .Map(x => x.Single(a => a.Delivery))
                    .Recover<ArgumentException>(x => new AddressStub(nameof(ArgumentException) + x.Message, 15))
                    .Recover<InvalidOperationException>(x => new AddressStub(nameof(InvalidOperationException) + x.Message, expected))
                    .Recover<Exception>(x => new AddressStub(nameof(InvalidOperationException) + x.Message, 17))
                    .Map(x => x.Number)
                    .GetValue();

                Assert.Equal(expected, actual);
        }

        [Theory]
        [Category("Try")]
        [InlineData(true, 2)]
        [InlineData(false, 0)]
        public void Try_Should_ResolveFilterToTry(bool delivery, int expected)
        {
            var actual = MonadUtilities
                    .ReturnTry(new AddressStub("Some street", 13))
                    .Map(x => new CustomerStub("Some customer", x))
                    .Execute(x => x.Addresses.Add(new AddressStub("Some other street", 14, delivery)))
                    .Map(x => x.Addresses.ToList())
                    .Filter(x => x.Any(a => a.Delivery))
                    .Map(x => x.Count())
                    .GetValue();

            Assert.Equal(expected, actual);
        }
    }
}