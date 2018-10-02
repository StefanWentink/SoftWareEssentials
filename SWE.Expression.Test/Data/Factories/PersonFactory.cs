namespace SWE.Expression.Test.Data.Factories
{
    using System.Collections.Generic;

    using SWE.Expression.Test.Data.Models;

    internal static class PersonFactory
    {
        internal static IEnumerable<PersonStub> List { get; } = new List<PersonStub>
                                                             {
                                                                 new PersonStub("Jan", "Jansen"),
                                                                 new PersonStub("Henk", "Jansen"),
                                                                 new PersonStub("Piet", "Visser")
                                                             };
    }
}