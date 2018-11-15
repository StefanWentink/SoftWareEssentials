namespace SWE.Contract.Interfaces.Containers
{
    using SWE.Contract.Enums;
    using SWE.Contract.Interfaces.Handlers;

    public interface IOperationHandlerContainer<T>
    {
        IValidationResultHandler<T> GetHandlerContainer(OrderDirective orderDirective, OperationDirective operationDirective);
    }
}