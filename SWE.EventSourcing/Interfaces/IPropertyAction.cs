namespace SWE.EventSourcing.Interfaces
{
    using System;

    public interface IPropertyAction<T>
    {
        Action<T> GetPreviousValueAction();

        Action<T> GetValueAction();
    }
}