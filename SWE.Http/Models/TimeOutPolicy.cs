using SWE.Http.Interfaces;

namespace SWE.Http.Models.Policies
{
    public class TimeOutPolicy : ITimeOutPolicy
    {
        public int TimeOutMilliseconds { get; }

        public TimeOutPolicy(int milliseconds)
        {
            TimeOutMilliseconds = milliseconds;
        }
    }

    public class TimeOutPolicy<T> : TimeOutPolicy, ITimeOutPolicy<T>
    {
        public TimeOutPolicy(int milliseconds)
            : base(milliseconds)
        {
        }
    }
}