namespace SWE.Http.Models
{
    using System;
    using System.Text;

    public class BasicToken : SecurityToken
    {
        public BasicToken(string userName, string password)
            : base("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}")))
        { }
    }
}