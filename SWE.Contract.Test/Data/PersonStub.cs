namespace SWE.Contract.Test.Data
{
    using System;

    internal class PersonStub
    {
        internal string FirstName { get; set; }

        internal string Initials { get; set; }

        internal string LastName { get; set; }

        internal DateTime BirthDay { get; set; }

        internal PersonStub(string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthday;
        }
    }
}