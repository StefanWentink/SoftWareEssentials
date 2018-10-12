namespace SWE.Swagger.DocumentFilters
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Used in Startup
    /// services.AddSwaggerGen(options =>
    ///     {
    ///         options.SwaggerDoc("v1", new Info { Title = "Title", Version = "v1" });
    ///         options.DocumentFilter<ODataDocumentFilter>("odata");
    ///     });
    /// Let swagger create some usefull documentation.
    /// </summary>
    public class ODataDocumentFilter : IDocumentFilter
    {
        private string Prefix { get; }

        public ODataDocumentFilter()
            : this("odata")
        {
        }

        /// <summary>
        /// Initializes a instance of <see cref="ODataDocumentFilter"/>.
        /// </summary>
        /// <param name="prefix">Prefix of api. http(s)://{ip}:{port}/<see cref="prefix"/>/{controller}</param>
        public ODataDocumentFilter(string prefix)
        {
            Prefix = prefix;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var derivedType = typeof(ODataController);

            foreach (var controllerType in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t != derivedType && derivedType.IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract))
            {
                var controllerName = controllerType.Name.Replace("Controller", string.Empty);

                foreach (var method in controllerType.GetMethods()
                    .Where(x =>
                        x.CustomAttributes.Any(a => typeof(HttpMethodAttribute).IsAssignableFrom(a.AttributeType))
                        || x.CustomAttributes.Any(a => typeof(EnableQueryAttribute).IsAssignableFrom(a.AttributeType) || typeof(EnableQueryAttribute) == a.AttributeType)))
                {
                    var path = $"/{Prefix}/{controllerName}/{method.Name}({string.Join(", ", method.GetParameters().Select(x => $"{x.ParameterType} {x.Name}"))})";

                    var operation = new Operation
                    {
                        Tags = new List<string> { "OData" },
                        OperationId = $"OData_{controllerName}",

                        Summary = "Summary about method / data",
                        Description = "Description / options for the call.",

                        Consumes = new List<string>(),
                        Produces = new List<string> { "application/atom+xml", "application/json", "text/json", "application/xml", "text/xml" },
                        Deprecated = false
                    };

                    var response = new Response() { Description = "OK" };
                    response.Schema = new Schema { Type = "array", Items = context.SchemaRegistry.GetOrRegister(method.ReturnType) };
                    operation.Responses = new Dictionary<string, Response> { { "200", response } };

                    var security = GetSecurityForOperation(controllerType);
                    if (security != null)
                    {
                        operation.Security = new List<IDictionary<string, IEnumerable<string>>> { security };
                    }

                    try
                    {
                        swaggerDoc.Paths.Add(path, new PathItem() { Get = operation });
                    }
                    catch { }
                }
            }
        }

        private static Dictionary<string, IEnumerable<string>> GetSecurityForOperation(MemberInfo controller)
        {
            if (controller.GetCustomAttribute(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute)) != null)
            {
                return new Dictionary<string, IEnumerable<string>> { { "oauth2", new[] { "actioncenter" } } };
            }

            return null;
        }
    }
}