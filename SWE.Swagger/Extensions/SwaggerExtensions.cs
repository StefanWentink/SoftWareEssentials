namespace SWE.Swagger.Extensions
{
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class SwaggerExtensions
    {
        internal static string GetPath(string prefix, Type controllerType, MethodInfo methodInfo)
        {
            var controllerName = controllerType.Name.Replace("Controller", string.Empty);

            return $"/{prefix}/{controllerName}/{methodInfo.Name}({string.Join(", ", methodInfo.GetParameters().Select(x => $"{x.ParameterType} {x.Name}"))})";
        }

        internal static Operation GetOperation(string controllerName)
        {
            return new Operation
            {
                Tags = new List<string> { "OData" },
                OperationId = $"OData_{controllerName}",

                Summary = "Summary about method / data",
                Description = "Description / options for the call.",

                Consumes = new List<string>(),
                Produces = new List<string> { "application/atom+xml", "application/json", "text/json", "application/xml", "text/xml" },
                Deprecated = false
            };
        }

        internal static void SetOperation(this Operation operation, Schema schema, IDictionary<string, IEnumerable<string>> security)
        {
            var response = new Response() { Description = "OK" };
            response.Schema = new Schema { Type = "array", Items = schema };
            operation.Responses = new Dictionary<string, Response> { { "200", response } };

            SetSecurity(operation, security);
        }

        internal static void SetSecurity(this Operation operation, IDictionary<string, IEnumerable<string>> security)
        {
            if (security != null)
            {
                operation.Security = new List<IDictionary<string, IEnumerable<string>>> { security };
            }
        }

        internal static Dictionary<string, IEnumerable<string>> GetSecurityForOperation(this MemberInfo memberInfo)
        {
            if (memberInfo.GetCustomAttribute(typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute)) != null)
            {
                return new Dictionary<string, IEnumerable<string>> { { "oauth2", new[] { "actioncenter" } } };
            }

            return null;
        }
    }
}