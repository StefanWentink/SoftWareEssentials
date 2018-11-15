namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.EventArgs;
    using System;

    public abstract class ValidationHandler<T> : ValidationResultHandler<T>
    {
        private Func<T, bool> ValidCondition { get; set; }

        private Func<T, string> MessageFunction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationHandler"/> class.
        /// </summary>
        /// <param name="validCondition"></param>
        /// <exception cref="ArgumentNullException">If <see cref="validCondition"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If <see cref="messageFunction"/> is null.</exception>
        protected ValidationHandler(Func<T, bool> validCondition, Func<T, string> messageFunction)
        {
            ValidCondition = validCondition ??
                throw new ArgumentNullException(nameof(validCondition), $"{nameof(validCondition)} must not be null.");

            MessageFunction = messageFunction ??
                throw new ArgumentNullException(nameof(messageFunction), $"{nameof(messageFunction)} must not be null.");
        }

        public override void Execute(T value)
        {
            if (!ValidCondition(value))
            {
                OnInvalidResult(new ValidationHandlerArgs(MessageFunction(value)));
            }
        }

        protected override void DisposeHandler()
        {
            MessageFunction = null;
            ValidCondition = null;
        }
    }
}