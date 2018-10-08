namespace SWE.Expression.Test.Data.Models
{
    using System;
    using SWE.BasicType.Date.Extensions;

    internal class PersonStub
    {
        internal string FirstName { get; set; }

        internal string LastName { get; set; }

        internal int Age { get; set; }

        internal PersonStub(string firstName, string lastName)
            : this(
                  firstName,
                  lastName,
                  new DateTime(1981, 7, 3).GetAge(DateTime.Now))
        {
        }

        internal PersonStub(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}