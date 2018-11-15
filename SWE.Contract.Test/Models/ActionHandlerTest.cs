namespace SWE.Contract.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Contract.Models.Handlers;
    using SWE.Contract.Test.Data;
    using SWE.Xunit.Attributes;
    using System;

    public class ActionHandlerTest
    {
        [Theory]
        [Category("ActionHandler")]
        [InlineData("Stefan", "T", "S")]
        [InlineData("Stefan", "", "S")]
        [InlineData("Stefan", " ", "S")]
        [InlineData("Stefan", null, "S")]
        public void Execute_Should_ApplyAction(string firstName, string setInitials, string expectedInitials)
        {
            var handler = new ActionHandler<PersonStub>(x => x.Initials = x.FirstName.Substring(0, 1));

            var person = PersonFactory.Create(firstName, setInitials);
            person.Initials.Should().Be(setInitials);
            handler.Execute(person);
            person.Initials.Should().Be(expectedInitials);
        }

        [Theory]
        [Category("ActionHandler")]
        [InlineData("Stefan", "T", "T")]
        [InlineData("Stefan", "", "")]
        [InlineData("Stefan", " ", " ")]
        [InlineData("Stefan", null, "S")]
        [InlineData(" ", null, " ")]
        [InlineData("", null, null)]
        [InlineData(null, null, null)]
        public void Execute_Should_ApplyAction_With_ConditionFullFilled(string firstName, string setInitials, string expectedInitials)
        {
            var handler = new ActionHandler<PersonStub>(
                x => x.Initials = x.FirstName.Substring(0, 1),
                x => x.Initials == default && (x.FirstName?.Length ?? 0) > 0);

            var person = PersonFactory.Create(firstName, setInitials);
            person.Initials.Should().Be(setInitials);
            handler.Execute(person);
            person.Initials.Should().Be(expectedInitials);
        }

        [Fact]
        [Category("ActionHandler")]
        public void Constructor_Should_RaiseArgumentNullException_When_ActionIsNull()
        {
            Action action = () => new ActionHandler<PersonStub>(null);
            action.Should().Throw<ArgumentNullException>();
        }
    }
}