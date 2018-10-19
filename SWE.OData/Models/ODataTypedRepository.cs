namespace SWE.OData.Models
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SWE.Http.Interfaces;
    using SWE.Http.Models;
    using System.Collections.Generic;

    public class ODataTypedRepository<T> : TypedRepository<T>
    {
        public ODataTypedRepository(
            ILogger logger,
            IExchanger exchanger,
            IActions actions,
            ITimeOutPolicy<T> policy)
            : base(logger, exchanger, actions, policy)
        {
        }

        protected override List<T> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<ODataValues<T>>(value).Value;
        }
    }
}