namespace SWE.Contract.Test.Models
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.Contract.Enums;
    using SWE.Contract.Models.Handlers;
    using SWE.Contract.Test.Data;
    using SWE.Xunit.Attributes;
    using System;
    using System.Linq;

    public class OperationHandlerTest
    {
        [Fact]
        [Category("OperationHandler")]
        public void Constructor_Should_RaiseArgumentNullException_When_HandlerIsNull()
        {
            Action action = () => new OperationHandler<PersonStub>(
                null,
                OrderDirective.Undefined,
                OperationDirective.Undefined);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        [Category("OperationHandler")]
        public void Constructor_Should_RaiseArgumentException_When_OrderDirectiveIsInvalid()
        {
            Action action = () => new OperationHandler<PersonStub>(
                new ActionHandler<PersonStub>(x => x.Initials = x.FirstName.Substring(0, 1)),
                OrderDirective.Undefined,
                new[] { OperationDirective.Create, OperationDirective.Undefined });

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Category("OperationHandler")]
        public void Constructor_Should_RaiseArgumentException_When_OperationDirectiveIsNull()
        {
            Action action = () => new OperationHandler<PersonStub>(
                new ActionHandler<PersonStub>(x => x.Initials = x.FirstName.Substring(0, 1)),
                OrderDirective.Pre,
                null);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Category("OperationHandler")]
        public void Constructor_Should_RaiseArgumentException_When_OperationDirectiveContainsNoValidValue()
        {
            Action action = () => new OperationHandler<PersonStub>(
                new ActionHandler<PersonStub>(x => x.Initials = x.FirstName.Substring(0, 1)),
                OrderDirective.Pre,
                new[] { OperationDirective.Undefined });

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Category("OperationHandler")]
        public void Constructor_Should_CreateOperationHandler()
        {
            var handler = new OperationHandler<PersonStub>(
                new ActionHandler<PersonStub>(x => x.Initials = x.FirstName.Substring(0, 1)),
                OrderDirective.Pre,
                new[] { OperationDirective.Create, OperationDirective.Undefined, OperationDirective.Update });

            handler.OperationDirectives.Count().Should().Be(2);
            handler.OrderDirective.Should().Be(OrderDirective.Pre);
        }
    }
}