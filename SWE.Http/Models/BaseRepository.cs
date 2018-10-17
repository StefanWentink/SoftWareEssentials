namespace SWE.Http.Models
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SWE.Http.Enums;
    using SWE.Http.Interfaces;

    using System.Text;

    public abstract class BaseRepository
    {
        protected ILogger Logger { get; }

        protected IExchanger Exchanger { get; }

        protected IActions Actions { get; }

        protected BaseRepository(ILogger<BaseRepository> logger, IExchanger exchanger, IActions actions)
        {
            Logger = logger;
            Exchanger = exchanger;
            Actions = actions;
        }

        protected void LogSerializationException(JsonSerializationException exception, IRequest request, string result)
        {
            Logger.LogCritical(exception, $"Deserializing {request.Route} failed.");
            Logger.LogError("Deserialization of response failed");
            Logger.LogError($"Target type: {request.Route}");
            Logger.LogError($"Failed request: [{request.Verb}] {request.Route}/{request.Action}");
            Logger.LogError($"Request content: {request.Content}");
            Logger.LogError($"Response: {result}");
        }

        protected virtual IRequest CreateRequest(string route, string action, string content, HttpVerb verb, Encoding encoding)
        {
            return new Request(route, action, content, verb, encoding);
        }
    }
}