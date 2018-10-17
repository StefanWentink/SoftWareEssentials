namespace SWE.Http.Interfaces
{
    public interface ITimeOutPolicy
    {
        int TimeOutMilliseconds { get; }
    }

    public interface ITimeOutPolicy<T> : ITimeOutPolicy
    {
    }
}