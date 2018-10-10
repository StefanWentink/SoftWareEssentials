namespace SWE.Expression.Test.Data.Models
{
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