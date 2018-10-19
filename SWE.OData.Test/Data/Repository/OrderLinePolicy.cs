namespace SWE.OData.Test.Data.Repository
{
    using Microsoft.Extensions.Logging;
    using SWE.Http.Interfaces;
    using SWE.Polly.Models.Policies;

    internal class OrderLinePolicy : PollyRetryPolicy, ITimeOutPolicy<OrderLine>
    {
        public OrderLinePolicy(ILogger logger)
            : base(logger, 10000, 10000, 10, 1000)
        {
        }
    }
}