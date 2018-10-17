namespace SWE.OData.Interfaces
{
    using SWE.Builder.Interfaces;

    public interface IODataBasicBuilder : IBuilder<string, object>
    {
        string BuilderKey();
    }
}