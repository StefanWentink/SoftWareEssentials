namespace SWE.BasicType.Extensions
{
    using global::Xunit;
    using SWE.Xunit.Attributes;
    using System;
    using System.Linq;

    public class ExceptionExtensionsTest
    {
        [Fact]
        [Category("ExceptionExtensions")]
        public void ToInnerMostException_Should_ReturnNull_When_ExceptionIsNull()
        {
            Exception exception = null;
            Assert.Null(exception.GetInnerMostException());
        }

        [Fact]
        [Category("ExceptionExtensions")]
        public void ToInnerMostException_Should_ReturnException_When_ExceptionIsSingle()
        {
            var exception = new NullReferenceException();
            Assert.IsType<NullReferenceException>(exception.GetInnerMostException());
        }

        [Fact]
        [Category("ExceptionExtensions")]
        public void ToInnerMostExceptions_Should_ReturnExceptionStackOfThree_When_TwoInnerExceptions()
        {
            var exception = new NullReferenceException(string.Empty, new ArgumentNullException(string.Empty, new InvalidOperationException()));
            var result = exception.GetInnerExceptions();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        [Category("ExceptionExtensions")]
        public void ToInnerMostExceptions_Should_ReturnInnerMostException_When_TwoInnerExceptions()
        {
            var exception = new NullReferenceException(string.Empty, new ArgumentNullException(string.Empty, new InvalidOperationException()));
            var result = exception.GetInnerMostException();
            Assert.IsType<InvalidOperationException>(result);
        }
    }
}