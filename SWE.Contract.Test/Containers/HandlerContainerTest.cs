namespace SWE.Contract.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.BasicType.Utilities;
    using SWE.Contract.Enums;
    using SWE.Contract.EventArgs;
    using SWE.Contract.Models.Containers;
    using SWE.Contract.Models.Handlers;
    using SWE.Contract.Test.Data;
    using SWE.Xunit.Attributes;
    using System.Collections.Generic;

    public class OperationHandlerContainerTest
    {
        private List<OperationHandler<PersonStub>> OperationHandlers { get; } =
            new List<OperationHandler<PersonStub>>
            {
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.FirstName = "Pre"), OrderDirective.Pre),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.FirstName = "Post"), OrderDirective.Post),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PreCreate"), OrderDirective.Pre, OperationDirective.Create),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PreUpdate"), OrderDirective.Pre, OperationDirective.Update),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PreDelete"), OrderDirective.Pre, OperationDirective.Delete),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PostCreate"), OrderDirective.Post, OperationDirective.Create),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PostUpdate"), OrderDirective.Post, OperationDirective.Update),
                new OperationHandler<PersonStub>(new ActionHandler<PersonStub>(x => x.LastName = "PostDelete"), OrderDirective.Post, OperationDirective.Delete),

                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.FirstName != "Pre", x => x.FirstName), OrderDirective.Pre),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.FirstName != "Post", x => x.FirstName), OrderDirective.Post),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PreCreate", x => x.LastName), OrderDirective.Pre, OperationDirective.Create),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PreUpdate", x => x.LastName), OrderDirective.Pre, OperationDirective.Update),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PreDelete", x => x.LastName), OrderDirective.Pre, OperationDirective.Delete),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PostCreate", x => x.LastName), OrderDirective.Post, OperationDirective.Create),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PostUpdate", x => x.LastName), OrderDirective.Post, OperationDirective.Update),
                new OperationHandler<PersonStub>(new ValidationHandler<PersonStub>(x => x.LastName != "PostDelete", x => x.LastName), OrderDirective.Post, OperationDirective.Delete),
            };

        [Theory]
        [Category("OperationHandlerContainer")]
        [InlineData(OrderDirective.Pre, OperationDirective.Create)]
        [InlineData(OrderDirective.Pre, OperationDirective.Update)]
        [InlineData(OrderDirective.Pre, OperationDirective.Delete)]
        [InlineData(OrderDirective.Post, OperationDirective.Create)]
        [InlineData(OrderDirective.Post, OperationDirective.Update)]
        [InlineData(OrderDirective.Post, OperationDirective.Delete)]
        public void Execute_Should_ReturnCorrectNumberOfMessages(OrderDirective order, OperationDirective operation)
        {
            var handler = new OperationHandlerContainer<PersonStub>(OperationHandlers);
            var expectedFirstName = EnumUtilities.GetDescription(order);
            var expectedLastName = expectedFirstName + EnumUtilities.GetDescription(operation);

            var container = handler.GetHandlerContainer(order, operation);
            var person = PersonFactory.CreateEmpty();
            var messages = new List<string>();
            container.InvalidResult += (object sender, ValidationHandlerArgs e) => messages.Add(e.ValidationResult.ErrorMessage);

            container.Execute(person);

            person.FirstName.Should().Be(expectedFirstName);
            person.LastName.Should().Be(expectedLastName);
            messages.Count.Should().Be(2);
            messages.Should().Contain(expectedFirstName);
            messages.Should().Contain(expectedLastName);
        }
    }
}