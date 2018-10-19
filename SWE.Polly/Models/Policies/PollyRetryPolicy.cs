namespace SWE.Polly.Models.Policies
{
    using global::Polly;
    using global::Polly.CircuitBreaker;
    using Microsoft.Extensions.Logging;
    using SWE.Polly.Interfaces.Policies;
    using System;

    public class PollyRetryPolicy : PollyTimeOutPolicy, IPollyPolicy
    {
        public PollyRetryPolicy(
            ILogger logger,
            int timeOutMilliseconds,
            int retryMilliseconds,
            int circuitBreakerRetries,
            int circuitBreakerDelayMiliseconds)
            : base(logger, timeOutMilliseconds)
        {
            var waitAndRetryPolicy = Policy
                .Handle<Exception>(exception => !(exception is BrokenCircuitException))
                .WaitAndRetryForeverAsync(
                    x => TimeSpan.FromMilliseconds(retryMilliseconds),
                    (exception, calculatedWaitDuration) =>
                    logger.LogError($".Exception Log : {exception.Message } : retry in {calculatedWaitDuration}."));

            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: circuitBreakerRetries,
                    durationOfBreak: TimeSpan.FromMilliseconds(circuitBreakerDelayMiliseconds),
                    onBreak: (exception, breakDelay) =>
                    {
                        logger.LogWarning($".CircuitBreaker: Breaking the circuit for {breakDelay.TotalMilliseconds} ms!");
                        logger.LogWarning($"..exception: {exception.Message}");
                    },
                    onReset: () => logger.LogWarning($".CircuitBreaker: Call ok! Closed the circuit again!"),
                    onHalfOpen: () => logger.LogWarning($".CircuitBreaker: Half-open: Next call is a trial!")
                );

            PolicyWrap = Policy.WrapAsync(waitAndRetryPolicy, circuitBreakerPolicy).WrapAsync(PolicyWrap);
        }
    }
}