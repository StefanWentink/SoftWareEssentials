namespace SWE.Contract.EventArgs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ValidationHandlerArgs : EventArgs
    {
        public ValidationResult ValidationResult { get; }

        public ValidationHandlerArgs(string message)
        {
            ValidationResult = new ValidationResult(message);
        }
    }
}