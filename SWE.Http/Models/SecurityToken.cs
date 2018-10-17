namespace SWE.Http.Models
{
    using SWE.Http.Interfaces;
    using System;

    public class SecurityToken : ISecurityToken
    {
        public string Schema { get; set; }

        public string Parameter { get; set; }

        [Obsolete("Only for serialization.", true)]
        public SecurityToken()
        {
        }

        public SecurityToken(string schema, string parameter)
        {
            Schema = schema;
            Parameter = parameter;
        }
    }
}