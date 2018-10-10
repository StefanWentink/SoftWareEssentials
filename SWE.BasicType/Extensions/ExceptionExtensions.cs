namespace SWE.BasicType.Extensions
{
    using SWE.BasicType.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ExceptionExtensions
    {
        public static Exception GetInnerMostException(this Exception exception)
        {
            return exception.GetInnerExceptions().LastOrDefault();
        }

        public static IEnumerable<Exception> GetInnerExceptions(this Exception exception)
        {
            var result = exception;

            while (!ObjectUtilities.IsDefault(result))
            {
                yield return result;
                result = result.InnerException;
            }
        }
    }
}