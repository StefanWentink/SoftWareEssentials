namespace SWE.OData.Test.Data.Repository
{
    using SWE.Http.Constants;
    using SWE.Http.Models;

    internal class OrderUriContainer : UriContainer
    {
        public OrderUriContainer()
            : base("https://localhost:44375", "odata", ExchangerConstants.ContentType, "/")
        {
        }
    }
}