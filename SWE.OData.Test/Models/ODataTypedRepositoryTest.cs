namespace SWE.OData.Test.Models
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SWE.Http.Interfaces;
    using SWE.OData.Models;
    using SWE.OData.Test.Data;
    using SWE.OData.Test.Data.Repository;
    using SWE.Polly.Models;
    using SWE.Xunit.Attributes;
    using System;
    using global::Xunit;

    public class ODataTypedRepositoryTest
    {
        [Fact]
        [Category("ODataTypedRepository")]
        public void ODataTypedRepository_Should_Resolve()
        {
            ILogger logger = new LoggerFactory()
                .AddDebug()
                .AddConsole()
                .CreateLogger<ODataTypedRepositoryTest>();

            // setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger>(new Logger())
                .AddSingleton<IExchanger, PolicyExchanger>()

                .AddSingleton<IRepository<Order>, ODataTypedRepository<Order>>()
                .AddSingleton<IRepository<OrderLine>, ODataTypedRepository<OrderLine>>()

                .AddSingleton<ITimeOutPolicy<Order>, OrderPolicy>()
                .AddSingleton<ITimeOutPolicy<OrderLine>, OrderLinePolicy>()

                .AddSingleton<IActions, OrderActions>()
                .AddSingleton<IUriContainer, OrderUriContainer>()
                .BuildServiceProvider();
            try
            {
                var repository = serviceProvider.GetService<IRepository<Order>>();
            }
            catch (Exception exception)
            {
                var loggerInstance = serviceProvider.GetService<ILogger>();
                loggerInstance.LogError(exception.Message);
            }
        }
    }
}