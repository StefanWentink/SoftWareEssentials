namespace SWE.Contract.Interfaces.Handlers
{
    using SWE.Contract.EventArgs;
    using System;

    public interface IValidationResultHandler<T> : IHandler<T>
    {
        /// <summary>
        /// If condition not valid.
        /// </summary>
        event EventHandler<ValidationHandlerArgs> InvalidResult;
    }
}