namespace SWE.OData.Test.Data.Repository
{
    using SWE.Http.Models;

    internal class OrderActions : Actions
    {
        public OrderActions()
        : base("Create", string.Empty, "Update", "Delete")
        { }
    }
}