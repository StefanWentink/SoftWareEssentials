namespace SWE.Polly.Interfaces.Policies
{
    using global::Polly;

    public interface IPollyPolicy : Http.Interfaces.ITimeOutPolicy
    {
        Policy PolicyWrap { get; }
    }

    public interface IPollyPolicy<T> : IPollyPolicy, Http.Interfaces.ITimeOutPolicy<T>
    {
    }
}