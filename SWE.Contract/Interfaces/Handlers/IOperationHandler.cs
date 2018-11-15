namespace SWE.Contract.Interfaces.Handlers
{
    using SWE.Contract.Enums;
    using SWE.Contract.Interfaces.Handlers;
    using System.Collections.Generic;

    public interface IOperationHandler<T>
    {
        IEnumerable<OperationDirective> OperationDirectives { get; }

        OrderDirective OrderDirective { get; }

        IHandler<T> Handler { get; }
    }
}