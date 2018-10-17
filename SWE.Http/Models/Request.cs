namespace SWE.Http.Models
{
    using Enums;
    using Interfaces;
    using System.Text;

    public class Request : IRequest
    {
        public string Route { get; set; }

        public string Action { get; set; }

        public string Content { get; set; }

        public HttpVerb Verb { get; set; }

        public Encoding Encoding { get; set; }

        public Request(string route, string action, string content, HttpVerb verb)
            : this(route, action, content, verb, Encoding.UTF8)
        {
        }

        public Request(string route, string action, string content, HttpVerb verb, Encoding encoding)
        {
            Route = route;
            Action = action;
            Content = content;
            Verb = verb;
            Encoding = encoding;
        }
    }
}