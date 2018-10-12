namespace SWE.Swagger.DocumentFilters
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;
    using System.Reflection;
    using SWE.Reflection.Extensions;
    using SWE.Reflection.Utilities;
    using System.Collections.Generic;
    using System;
    using SWE.Swagger.Extensions;

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

            foreach (var controllerType in Assembly.GetExecutingAssembly().GetAllInstanceTypes(derivedType))
            {
                var controllerName = controllerType.Name.Replace("Controller", string.Empty);

                foreach (var methodInfo in controllerType.GetMethods()
                    .Where(x =>
                    CustomAttributeUtilities.GetCustomAttribute(x, typeof(HttpMethodAttribute)).Any()
                    || CustomAttributeUtilities.GetCustomAttribute(x, typeof(EnableQueryAttribute)).Any()))
                {
                    var path = GetPath(Prefix, controllerType, methodInfo);

                    var operation = GetOperation(controllerName);

                    var schema = context.SchemaRegistry.GetOrRegister(methodInfo.ReturnType);
                    var security = GetSecurityForOperation(methodInfo);

                    operation.SetOperation(schema, security);

                    try
                    {
                        swaggerDoc.Paths.Add(path, new PathItem() { Get = operation });
                    }
                    catch { }
                }
            }
        }

        public virtual string GetPath(string prefix, Type controllerType, MethodInfo methodInfo)
        {
            return SwaggerExtensions.GetPath(prefix, controllerType, methodInfo);
        }

        public virtual Operation GetOperation(string controllerName)
        {
            return SwaggerExtensions.GetOperation(controllerName);
        }

        public virtual Dictionary<string, IEnumerable<string>> GetSecurityForOperation(MemberInfo memberInfo)
        {
            return memberInfo.GetSecurityForOperation();
        }
    }
}