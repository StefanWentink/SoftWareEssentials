namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.EventArgs;
    using System;

    public abstract class ValidationResultHandler<T> : Handler<T>
    {
        /// <summary>
        /// If condition not valid.
        /// </summary>
        public event EventHandler<ValidationHandlerArgs> InvalidResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultHandler"/> class.
        /// </summary>
        protected ValidationResultHandler()
        {
        }

        protected virtual void OnInvalidResult(ValidationHandlerArgs e)
        {
            InvalidResult?.Invoke(this, e);
        }
    }
}