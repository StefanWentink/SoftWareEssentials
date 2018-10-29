namespace SWE.Http.Interfaces
{
    public interface ITimeOutPolicy
    {
        int TimeOutMilliseconds { get; set; }
    }

    public interface ITimeOutPolicy<T> : ITimeOutPolicy
    {
    }
}