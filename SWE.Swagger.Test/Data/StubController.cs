namespace SWE.Swagger.Test.Data
{
    using System;

    internal class StubController
    {
        public string Get(Guid id, bool single)
        {
            return id.ToString() + single.ToString();
        }

        public void Clear()
        {
            Console.WriteLine("Clear");
        }

        public void Delete(Guid id)
        {
            Console.WriteLine($"Delete {id}.");
        }
    }
}