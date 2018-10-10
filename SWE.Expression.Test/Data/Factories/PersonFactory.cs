namespace SWE.Expression.Test.Data.Factories
{
    using SWE.Expression.Test.Data.Models;
    using System.Collections.Generic;

    internal static class PersonFactory
    {
        internal static IEnumerable<PersonStub> List { get; }
            = new List<PersonStub>
            {
                new PersonStub("Jan", "Jansen"),
                new PersonStub("Henk", "Jansen"),
                new PersonStub("Piet", "Visser")
            };
    }
}