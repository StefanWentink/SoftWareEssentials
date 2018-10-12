namespace SWE.Reflection.Test.Data
{
    using System;

    public class DerivedStub : Stub
    {
        public override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}