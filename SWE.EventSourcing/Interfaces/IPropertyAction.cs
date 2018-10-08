namespace SWE.EventSourcing.Interfaces
{
    using System;

    public interface IPropertyAction<T>
    {
        /// <summary>
        /// Get action revert.
        /// </summary>
        /// <returns></returns>
        Action<T> GetRevertValueAction();

        /// <summary>
        /// Get action apply.
        /// </summary>
        /// <returns></returns>
        Action<T> GetApplyValueAction();
    }
}