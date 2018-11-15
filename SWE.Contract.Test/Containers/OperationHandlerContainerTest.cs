namespace SWE.Contract.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Contract.Interfaces.Handlers;
    using SWE.Contract.Models.Containers;
    using SWE.Contract.Models.Handlers;
    using SWE.Contract.Test.Data;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;

    public class HandlerContainerTest
    {
        [Theory]
        [Category("HandlerContainer")]
        [InlineData("Stefan", "T", 0)]
        [InlineData("Stefan", null, 0)]
        [InlineData("Stefan", "", 1)]
        [InlineData("Stefan", " ", 1)]
        [InlineData("", "T", 1)]
        [InlineData(null, "T", 1)]
        [InlineData("", null, 2)]
        [InlineData(null, null, 2)]
        public void Execute_Should_ReturnCorrectNumberOfMessages(string firstName, string intitials, int numberOfMessages)
        {
            var messages = Execute(PersonFactory.Create(firstName, intitials));

            messages.Count.Should().Be(numberOfMessages);
        }

        private List<string> Execute(PersonStub person)
        {
            var handler = new HandlerContainer<PersonStub>(
                new List<IHandler<PersonStub>> {
                    new ValidationHandler<PersonStub>(
                        x => !string.IsNullOrWhiteSpace(x.FirstName),
                        x => $"{nameof(PersonStub.FirstName)} [{x.FirstName}] should not be null or whitespace."),
                    new ActionHandler<PersonStub>(
                        x => x.Initials = x.FirstName.Substring(0, 1),
                        x => x.Initials == default && (x.FirstName?.Length ?? 0) > 0),
                    new ValidationHandler<PersonStub>(
                        x => !string.IsNullOrWhiteSpace(x.Initials),
                        x => $"{nameof(PersonStub.Initials)} [{x.Initials}] should not be null or whitespace.")
                });

            var messages = new List<string>();

            handler.InvalidResult += (object sender, Contract.EventArgs.ValidationHandlerArgs e) =>
                messages.Add(e.ValidationResult.ErrorMessage);

            handler.Execute(person);

            return messages;
        }
    }
}