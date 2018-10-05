namespace SWE.Expression.Test.Data.Models
{
    internal class PersonStub
    {
        internal string FirstName { get; set; }

        internal string LastName { get; set; }

        internal PersonStub(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}