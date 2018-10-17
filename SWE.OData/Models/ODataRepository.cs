namespace SWE.OData.Models
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SWE.Http.Interfaces;
    using SWE.Http.Models;
    using System.Collections.Generic;

    public class ODataRepository : Repository
    {
        public ODataRepository(ILogger<Repository> logger, IExchanger exchanger, IActions actions)
            : base(logger, exchanger, actions)
        {
        }

        protected override List<T> Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<ODataValues<T>>(value).Value;
        }
    }
}