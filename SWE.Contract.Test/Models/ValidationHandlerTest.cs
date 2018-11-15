namespace SWE.Contract.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Contract.Interfaces.Handlers;
    using SWE.Contract.Models.Handlers;
    using SWE.Contract.Test.Data;
    using SWE.Xunit.Attributes;
    using System;

    public class ValidationHandlerTest
    {
        [Fact]
        [Category("ValidationHandler")]
        public void Execute_Should_NotReturnMessage()
        {
            var message = Execute(PersonFactory.Create());
            message.Should().Be(string.Empty);
        }

        [Theory]
        [Category("ValidationHandler")]
        [InlineData("  ")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Execute_Should_ReturnMessage(string firstName)
        {
            var message = Execute(PersonFactory.Create(firstName));

            message.Should().Be($"{nameof(PersonStub.FirstName)} [{firstName}] should not be null or whitespace.");
        }

        private string Execute(PersonStub person)
        {
            var handler = new ValidationHandler<PersonStub>(
                x => !string.IsNullOrWhiteSpace(x.FirstName),
                x => $"{nameof(PersonStub.FirstName)} [{x.FirstName}] should not be null or whitespace.");

            var message = string.Empty;

            handler.InvalidResult += (object sender, Contract.EventArgs.ValidationHandlerArgs e) =>
                message = e.ValidationResult.ErrorMessage;

            handler.Execute(person);

            return message;
        }

        [Fact]
        [Category("ValidationHandler")]
        public void Constructor_Should_RaiseArgumentNullException_When_ConditionIsNull()
        {
            Action action = () => new ValidationHandler<PersonStub>(
                null,
                x => $"{nameof(PersonStub.FirstName)} [{x.FirstName}] should not be null or whitespace.");

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        [Category("ValidationHandler")]
        public void Constructor_Should_RaiseArgumentNullException_When_MessageIsNull()
        {
            Action action = () => new ValidationHandler<PersonStub>(
                x => string.IsNullOrWhiteSpace(x.FirstName),
                null);

            action.Should().Throw<ArgumentNullException>();
        }
    }
}