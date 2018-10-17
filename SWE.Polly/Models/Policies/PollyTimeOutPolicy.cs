namespace SWE.Polly.Models.Policies
{
    using Microsoft.Extensions.Logging;
    using global::Polly;
    using global::Polly.Timeout;
    using SWE.Polly.Interfaces.Policies;
    using System;

    public class PollyTimeOutPolicy : Http.Models.Policies.TimeOutPolicy, IPollyPolicy
    {
        public Policy PolicyWrap { get; protected set; }

        protected ILogger Logger { get; }

        public PollyTimeOutPolicy(ILogger<PollyTimeOutPolicy> logger, int milliseconds)
            : base(milliseconds)
        {
            Logger = logger;
            PolicyWrap = Policy.TimeoutAsync(
                TimeSpan.FromMilliseconds(milliseconds),
                TimeoutStrategy.Pessimistic);
        }
    }
}