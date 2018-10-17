namespace SWE.OData.Builders
{
    using SWE.Builder.Models;
    using SWE.OData.Interfaces;
    using System.Text;

    public class ODataEntityBuilder : Builder<string, object>, IODataBasicBuilder
    {
        public bool SubBuilder { protected get; set; }

        public string Entity { get; protected set; }

        public ODataEntityBuilder(string entity)
        {
            Entity = entity;
        }

        protected override string BuildResult()
        {
            var result = new StringBuilder();

            result.Append(Entity);
            result.Append("/");

            return result.ToString();
        }

        public virtual string BuilderKey()
        {
            return BuildResult();
        }
    }
}