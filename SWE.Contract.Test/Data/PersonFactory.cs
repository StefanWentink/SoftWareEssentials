namespace SWE.Contract.Test.Data
{
    using System;

    internal static class PersonFactory
    {
        internal static PersonStub Create()
        {
            return Create("Arnold");
        }

        internal static PersonStub Create(string firstName)
        {
            return Create(firstName, null);
        }

        internal static PersonStub Create(string firstName, string initials)
        {
            return new PersonStub(firstName, "Schwarzenegger", new DateTime(1947, 7, 30))
            {
                Initials = initials
            };
        }

        internal static PersonStub CreateEmpty()
        {
            return new PersonStub(default, default, new DateTime(1947, 7, 30))
            {
                Initials = default
            };
        }
    }
}