namespace SWE.Http.Models
{
    using SWE.Http.Interfaces;
    using System;

    public class Actions : IActions
    {
        public string Create { get; }
        public string Read { get; }
        public string Update { get; }
        public string Delete { get; }

        [Obsolete("Only for serialization.", true)]
        public Actions()
        { }

        public Actions(string create, string read, string update, string delete)
        {
            Create = create;
            Read = read;
            Update = update;
            Delete = delete;
        }
    }
}