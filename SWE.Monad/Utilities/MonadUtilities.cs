namespace SWE.Monad.Utilities
{
    using SWE.Monad.Interfaces;
    using SWE.Monad.Models;

    public static class MonadUtilities
    {
        public static ITry<T> ReturnTry<T>(T value)
        {
            return new Success<T>(value);
        }
    }
}