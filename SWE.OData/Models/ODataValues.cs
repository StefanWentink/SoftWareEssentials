namespace SWE.OData.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ODataValues<T>
    {
        [JsonProperty("odata.metadata")]
        public string Metadata { get; set; }

        public List<T> Value { get; set; }
    }
}