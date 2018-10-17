namespace SWE.OData.Interfaces
{
    public interface IODataEntityBuilder : IODataBasicBuilder
    {
        bool SubBuilder { set; }

        string Entity { get; }
    }
}