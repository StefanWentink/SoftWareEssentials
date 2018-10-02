namespace SWE.Expression.Test.Data.Models
{
    using System;

    internal class ContactPersonStub : PersonStub
    {
        internal int Id { get; set; }
        
        internal ContactPersonStub(int id, string firstName, string lastName)
            : base(firstName, lastName)
        {
            Id = id;
        }
    }
}
