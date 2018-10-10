namespace SWE.Expression.Test.Data.Factories
{
    using SWE.Expression.Test.Data.Models;
    using System.Collections.Generic;

    internal static class ContactPersonFactory
    {
        internal static IEnumerable<ContactPersonStub> List { get; } = new List<ContactPersonStub>
                                                             {
                                                                 new ContactPersonStub(1, "Jan", "Jansen"),
                                                                 new ContactPersonStub(2, "Henk", "Jansen"),
                                                                 new ContactPersonStub(3, "Piet", "Visser")
                                                             };
    }
}