namespace SWE.Http.Interfaces
{
    using Enums;

    public interface IRequest
    {
        string Route { get; set; }

        string Action { get; set; }

        string Content { get; set; }

        HttpVerb Verb { get; set; }
    }
}