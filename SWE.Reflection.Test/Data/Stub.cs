namespace SWE.Reflection.Test.Data
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Reflection;

    public class Stub : AbstractStub
    {
        [HttpGet]
        public override string GetString()
        {
            return "String";
        }

        [HttpPost]
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}